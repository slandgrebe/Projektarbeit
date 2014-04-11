// www.drdobbs.com/cpp/logging-in-c/201804215

#ifndef __LOG1_H__
#define __LOG1_H__

#include <sstream>
#include <string>
#include <stdio.h>

inline std::string NowTime();

enum TLogLevel { logOFF = 0, logFATAL, logERROR, logWARNING, logINFO, logDEBUG, logTRACE };

class Log {
public:
	Log();
	virtual ~Log();
	std::ostringstream& Get(TLogLevel level = logINFO);

	std::ostringstream& fatal();
	std::ostringstream& error();
	std::ostringstream& warning();
	std::ostringstream& info();
	std::ostringstream& debug();
	std::ostringstream& trace();
public:
	static TLogLevel& ReportingLevel();
	static std::string ToString(TLogLevel level);
	static TLogLevel FromString(const std::string& level);
protected:
	std::ostringstream os;
private:
	Log(const Log&);
	Log& operator =(const Log&);

	TLogLevel level;
};

inline Log::Log()
{
}

inline std::ostringstream& Log::Get(TLogLevel level)
{
	this->level = level;
	if (level <= Log::ReportingLevel()) {
		os << NowTime();
		os << " [" << ToString(level) << "]\t";
		//os << std::string(level > logDEBUG ? level - logDEBUG : 0, '\t');
	}
	return os;
}

inline std::ostringstream& Log::fatal() {
	return this->Get(logFATAL);
}
inline std::ostringstream& Log::error() {
	return this->Get(logERROR);
}
inline std::ostringstream& Log::warning() {
	return this->Get(logWARNING);
}
inline std::ostringstream& Log::info() {
	return this->Get(logINFO);
}
inline std::ostringstream& Log::debug() {
	return this->Get(logDEBUG);
}
inline std::ostringstream& Log::trace() {
	return this->Get(logTRACE);
}

inline Log::~Log()
{
	if (level <= Log::ReportingLevel() && os.str().length() > 0) {
		os << std::endl;
		fprintf(stderr, "%s", os.str().c_str());
		fflush(stderr);
	}
}

inline TLogLevel& Log::ReportingLevel()
{
	static TLogLevel reportingLevel = logDEBUG;
	return reportingLevel;
}

inline std::string Log::ToString(TLogLevel level)
{
	static const char* const buffer[] = { "OFF", "FATAL", "ERROR", "WARNING", "INFO", "DEBUG", "TRACE" };
	return buffer[level];
}

inline TLogLevel Log::FromString(const std::string& level)
{
	if (level == "TRACE")
		return logTRACE;
	if (level == "DEBUG")
		return logDEBUG;
	if (level == "INFO")
		return logINFO;
	if (level == "WARNING")
		return logWARNING;
	if (level == "ERROR")
		return logERROR;
	if (level == "FATAL")
		return logFATAL;
	if (level == "OFF")
		return logOFF;
	Log().warning() << "Unknown logging level '" << level << "'. Using INFO level as default.";
	return logINFO;
}

typedef Log FILELog;

#define LOG(level) \
if (level > Log::ReportingLevel()); \
	else Log().Get(level)

#if defined(WIN32) || defined(_WIN32) || defined(__WIN32__)

#include <windows.h>

inline std::string NowTime()
{
	const int MAX_LEN = 200;
	char buffer[MAX_LEN];
	if (GetTimeFormatA(LOCALE_USER_DEFAULT, 0, 0,
		"HH':'mm':'ss", buffer, MAX_LEN) == 0)
		return "Error in NowTime()";

	char result[100] = { 0 };
	static DWORD first = GetTickCount();
	//std::sprintf(result, "%s.%03ld", buffer, (long)(GetTickCount() - first) % 1000);
	sprintf_s(result, "%s.%03ld", buffer, (long)(GetTickCount() - first) % 1000);
	return result;
}

#else

#include <sys/time.h>

inline std::string NowTime()
{
	char buffer[11];
	time_t t;
	time(&t);
	tm r = { 0 };
	strftime(buffer, sizeof(buffer), "%X", localtime_r(&t, &r));
	struct timeval tv;
	gettimeofday(&tv, 0);
	char result[100] = { 0 };
	std::sprintf(result, "%s.%03ld", buffer, (long)tv.tv_usec / 1000);
	return result;
}

#endif //WIN32

#endif //__LOG_H__
