#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

#include <mutex>
#include <list>
#include <thread>
#include <sapi.h>
#include <iostream>
#include <atlbase.h>
#include <conio.h>
#include <string>
#pragma warning (disable:4996)
#include <sphelper.h>  

using namespace std;

namespace SapiVoice {	
	extern "C" {
		DLL_API void __cdecl initSpeech();
		DLL_API void __cdecl changeRate(int rate);
		DLL_API void __cdecl setText(char* text);
		DLL_API void __cdecl startSpeak();
		DLL_API void __cdecl setNowPause(int value);
		DLL_API void __cdecl setGender(char* value);
		DLL_API int __cdecl getNowPause();
		DLL_API void __cdecl pauseSpeak();
	}

	//define/init global members
	CComPtr<IEnumSpObjectTokens> cpIEnum;
	CComPtr<ISpObjectToken> cpToken;
	CComPtr <ISpVoice>	cpVoice;
	ULONG stream_number_;
	std::wstring utterance_;
	int char_position_;
	int prefix_len_;
	std::thread* theSpeechThread;
	wchar_t *wText;
	int counter = 0;
	int nowPause = 0;
	wchar_t *gender;

	//this method responsible to set the singer gender
	void setGender(char* text)
	{
		int len = strlen(text) + 1;
		gender = new wchar_t[len];

		memset(gender, 0, len);
		::MultiByteToWideChar(CP_ACP, NULL, text, -1, gender, len);
	}

	//indicate that speak is pause now
	void setNowPause(int value)
	{
		nowPause = value;
	}

	//pause the speak
	void pauseSpeak()
	{
		cpVoice->Pause();
	}

	//get the speak pause flag
	int getNowPause()
	{
		return nowPause;
	}

	//copy the input lyrics to text buffer
	void setText(char* text)
	{
		int len = strlen(text) + 1;
		wText = new wchar_t[len];

		memset(wText, 0, len);
		::MultiByteToWideChar(CP_ACP, NULL, text, -1, wText, len);
	}

	//main while
	//Checks each cycle which event is met
	void OnSpeechEvent() {

		SPEVENT event;
		while (S_OK == cpVoice->GetEvents(1, &event, NULL)) {
			if (event.ulStreamNum != stream_number_)
				continue;

			switch (event.eEventId) {
			case SPEI_START_INPUT_STREAM:								//start speak event
				std::cout << "SPEI_START_INPUT_STREAM" << std::endl;	
				break;
			case SPEI_END_INPUT_STREAM:									//end speak event
				char_position_ = utterance_.size();
				std::cout << "SPEI_END_INPUT_STREAM " << char_position_ << std::endl;
				break;
			case SPEI_WORD_BOUNDARY:									//start word event
				if (nowPause == 0 && wText[event.wParam + event.lParam] == '.')
				{
					cout << "End Sentence" << endl;
					nowPause = 1;
					cpVoice->Pause();
				}
				break;
			case SPEI_TTS_BOOKMARK:

				break;
			case SPEI_SENTENCE_BOUNDARY:

				break;
			}
		}
	}

	//define callback function
	static void __stdcall SpeechEventCallback(WPARAM w_param, LPARAM l_param);

	//necessary method for normal operation
	HRESULT WaitAndPumpMessagesWithTimeout(HANDLE hWaitHandle, DWORD dwMilliseconds)
	{
		HRESULT hr = S_OK;
		BOOL fContinue = TRUE;

		while (fContinue)
		{
			DWORD dwWaitId = ::MsgWaitForMultipleObjectsEx(1, &hWaitHandle, dwMilliseconds, QS_ALLINPUT, MWMO_INPUTAVAILABLE);
			switch (dwWaitId)
			{
			case WAIT_OBJECT_0:
			{
				fContinue = FALSE;
			}
			break;

			case WAIT_OBJECT_0 + 1:
			{
				MSG Msg;
				while (::PeekMessage(&Msg, NULL, 0, 0, PM_REMOVE))
				{
					::TranslateMessage(&Msg);
					::DispatchMessage(&Msg);
				}
			}
			break;

			case WAIT_TIMEOUT:
			{
				std::cout << "WAIT_TIMEOUT" << std::endl;
				hr = S_FALSE;
				fContinue = FALSE;
			}
			break;
			default:// Unexpected error
			{
				fContinue = FALSE;
				hr = E_FAIL;
			}
			break;
			}
		}
		return hr;
	}

	//init sapi object and creates a new thread
	void speechThreadFunc()
	{

		// Enumerate voice tokens that speak US English in a female voice.
		HRESULT hr2 = SpEnumTokens(SPCAT_VOICES, L"Language=409", gender, &cpIEnum);

		// Get the best matching token.
		if (SUCCEEDED(hr2))
		{
			hr2 = cpIEnum->Next(1, &cpToken, NULL);
		}

		if (FAILED(::CoInitialize(NULL)))
			return;

		std::cout << "initialized\n";

		//	cout << "creating voice\n";

		if (S_OK != cpVoice.CoCreateInstance(CLSID_SpVoice))
			return;
		std::cout << "voice initialized" << std::endl;

		//set the required event
		ULONGLONG event_mask =
			SPFEI(SPEI_START_INPUT_STREAM) |
			SPFEI(SPEI_TTS_BOOKMARK) |
			SPFEI(SPEI_SENTENCE_BOUNDARY) |
			SPFEI(SPEI_WORD_BOUNDARY) |
			SPFEI(SPEI_END_INPUT_STREAM);

		cpVoice->SetVoice(cpToken);

		cpVoice->SetInterest(event_mask, event_mask);

		std::cout << "interests set" << std::endl;

		cpVoice->SetNotifyCallbackFunction(SpeechEventCallback, 0, 0);

		std::cout << "callback function set" << std::endl;

		cpVoice->Speak(wText, SPF_ASYNC, &stream_number_);
		HANDLE hWait = cpVoice->SpeakCompleteEvent();
		WaitAndPumpMessagesWithTimeout(hWait, INFINITE);

		cpVoice.Release();
		::CoUninitialize();
	}

	void __stdcall SpeechEventCallback(WPARAM w_param, LPARAM l_param) {
		OnSpeechEvent();
	}

	//initialize the sapi object in new thread
	void initSpeech()
	{
		theSpeechThread = new std::thread(SapiVoice::speechThreadFunc);
	}

	//this method will change the speech rate
	void changeRate(int rate)
	{
		cpVoice->SetRate(rate);
		std::cout << "Rate changed: " << rate << std::endl;
	}

	//start speak after pause
	void startSpeak()
	{
		cpVoice->Resume();
	}

}