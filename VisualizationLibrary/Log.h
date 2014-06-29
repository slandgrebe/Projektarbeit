// www.drdobbs.com/cpp/logging-in-c/201804215

#ifndef __LOG1_H__
#define __LOG1_H__

#include <sstream>
#include <string>
#include <stdio.h>

#include <GL/glew.h>
#include <GLFW/glfw3.h>


/** Liefert die aktuelle Zeit zurück
* @author Stefan Landgrebe
* @return Text mit der aktuelle Zeit
*/
inline std::string NowTime();

/** Enumeration aller Loglevels
Zur Verfügung stehen: 
Level		| Bedeutung
-----------	| -----------
logOFF		| Deaktiviert das Log
logFATAL	| Nur fatale Meldungen werden angezeigt
logERROR	| Nur Fehlermeldungen und höher werden angezeigt
logWARNING	| Nur Warnungen und höher werden angezeigt
logINFO		| Nur Informationsmeldungen und höher werden angezeigt
logDEBUG	| Nur Debugmeldungen und höher werden angezeigt
logTRACE	| Alle Meldungen werden angezeigt
* @author Stefan Landgrebe
*/
enum TLogLevel { logOFF = 0, logFATAL, logERROR, logWARNING, logINFO, logDEBUG, logTRACE };


/** Log Klasse sollte als Alternative zu std::cout verwendet werden und erlaubt das selektive darstellen von unterschiedlich wichtigen Meldungen.
Der Code stammt ursprünglich von <a href="http://www.drdobbs.com/cpp/logging-in-c/201804215">http://www.drdobbs.com/cpp/logging-in-c/201804215</a>
Verwendung: Log().info() << "Dies ist eine Information";
Ausgabe: 13:26:17.172 [INFO]	Dies ist eine Information
* @author Stefan Landgrebe
*/
class Log {
public:

	/** Konstruktor 
	* @author Stefan Landgrebe
	*/
	Log();

	/** Destruktor
	Schreibt die Logmeldung.
	* @author Stefan Landgrebe
	*/
	virtual ~Log();

	/** Wird zur Ausgabe ins Log verwendet. Die Methode erwartet als Parameter das zu verwendende Loglevel.
	* @author Stefan Landgrebe
	* @param level Das zu verwendende Loglevel
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& Get(TLogLevel level = logINFO);


	/** Equivalänt zu Log().Get(logFATAL)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& fatal();

	/** Equivalänt zu Log().Get(logERROR)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& error();

	/** Equivalänt zu Log().Get(logWARNING)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& warning();

	/** Equivalänt zu Log().Get(logINFO)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& info();

	/** Equivalänt zu Log().Get(logDEBUG)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& debug();

	/** Equivalänt zu Log().Get(logTRACE)
	* @author Stefan Landgrebe
	* @return Referenz auf den stringstream
	*/
	std::ostringstream& trace();


	/** Definiert das aktuelle Loglevel. Nur Logmeldungen welche mindestens diesem Level entsprechen, werden ausgegeben.
	Verwendung: Log().ReportingLevel() = logINFO;
	* @author Stefan Landgrebe
	* @return Referenz auf das Loglevel
	*/
	static TLogLevel& ReportingLevel();

	/** Erzeugt den passenden Text zum mitgegebenen Loglevel
	* @author Stefan Landgrebe
	* @param level Loglevel
	* @return Loglevel als Text
	*/
	static std::string ToString(TLogLevel level);

	/** Liefert das entsprechende Loglevel zum mitgegebenen Loglevel zurück
	Umkehrung der ToString() Methode
	* @author Stefan Landgrebe
	* @param level Loglevel als Text
	* @return Loglevel
	* @see ToString()
	*/
	static TLogLevel FromString(const std::string& level);

	/** Hilfsfunktion um OpenGL Fehler zu finden
	* @author Stefan Landgrebe
	* @param file anzuzeigender Dateiname
	* @param line anzuzueigende Zeilennummer
	* @return Errorcode
	* @see http://www.lighthouse3d.com/cg-topics/error-tracking-in-opengl/
	*/
	static int printOglError(char *file, int line);
protected:
	std::ostringstream os; /** stringstream welcher für das Schreiben der Logmeldungen verwendet wird */
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


// Hilfsfunktion //http://www.lighthouse3d.com/cg-topics/error-tracking-in-opengl/
#define printOpenGLError() Log::printOglError(__FILE__, __LINE__)

inline int Log::printOglError(char *file, int line) {
	GLenum glErr;
	int    retCode = 0;

	glErr = glGetError();
	if (glErr != GL_NO_ERROR) {
		Log().debug() << "glError in file " << file << " @ line " << line << ": " << gluErrorString(glErr);
		retCode = 1;
	}
	return retCode;
}


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
	sprintf_s(result, "%s.%03ld", buffer, (long)(GetTickCount64() - first) % 1000);
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
