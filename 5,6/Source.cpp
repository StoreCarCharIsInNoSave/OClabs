//#include <vcl.h>
//#include <dir.h>
#include <direct.h>
#include <string>
#include <iostream>
#include <fstream>
#include <conio.h>
#include <cstdlib>
#include <windows.h>

#pragma hdrstop


#pragma argsused

std::string prNames
[30];
std::string prs[30];
int result = 0;
int count = 0;

std::string getPath()
{
    char dir[100];
    _getcwd(dir, 100);

    std::string currentWorkingDir = dir;

    for (size_t i = 0; i < currentWorkingDir.size(); i++)
    {
        if (currentWorkingDir[i] == '\\')
        {
            currentWorkingDir.insert(i, "\\");
            i++;
        }
    }

    currentWorkingDir += "\\\\Process.exe";
    return currentWorkingDir;
}

std::string toFileName(std::string str)
{
    std::string fName = "F";
    for (int i = 0; i < str.size(); i++)
    {
        if (str[i] != '.')
        {
            fName += str[i];
        }
        
    }
    return fName;
}



void createNewProcess(STARTUPINFO* sInfo, PROCESS_INFORMATION* pInfo, PROCESS_INFORMATION* pInfoParent, bool stop, int numPr)
{
    if (!stop)
    {
        ZeroMemory(sInfo, sizeof(STARTUPINFO));
        sInfo->cb = sizeof(STARTUPINFO);

        if (!CreateProcess(
            NULL,
            (LPSTR)getPath().c_str(),
            NULL,
            NULL,
            FALSE,
            CREATE_NEW_CONSOLE | NORMAL_PRIORITY_CLASS,
            NULL,
            NULL,
            sInfo,
            pInfo)
            )
        {
            std::cout << "Процесс не был создан!" << std::endl << "Проверьте имя процесса." << std::endl;
            return;
        }

        if (true)
        {
            std::cout << std::endl << "Запуск процесса " << prNames[numPr] << std::endl;

            STARTUPINFO si;
            PROCESS_INFORMATION pi;
            if (numPr == count - 1)
            {
                createNewProcess(&si, &pi, pInfo, true, -1);
            }
            else
            {
                createNewProcess(&si, &pi, pInfo, false, numPr + 1);
            }

            WaitForSingleObject(pInfo->hProcess, INFINITE); //Ожидает, пока указанный объект не перейдет в сигнальное состояние или пока не истечет время ожидания.

            std::cout << std::endl << "Завершение процесса " << prNames[numPr] << std::endl;

            std::ofstream file;
            file.open(toFileName(prNames[numPr]).c_str());

            int prevLen = 0;
            for (int i = 0; i < count; i++)
            {
                if (prs[i].find(prNames[numPr]) == 0)
                {
                    if (prevLen == 0)
                    {
                        file << prs[i];
                        prevLen = prs[i].size();
                        continue;
                    }
                    if (prevLen < prs[i].size())
                    {
                        file << "(" << prs[i];
                    }
                    else
                    {
                        if (prevLen > prs[i].size())
                        {
                            file << ")," << prs[i];
                        }
                        else
                        {
                            file << "," << prs[i];
                        }

                    }
                    prevLen = prs[i].size();
                }
            }

            if (prevLen - prNames[numPr].size() == 2)
            {
                file << ")";
            }
            else
                if (prevLen - prNames[numPr].size() == 4)
                {
                    file << "))";
                }

            file.close();

            ::result += prNames[numPr].size() / 2;

            CloseHandle(pInfo->hProcess);
            CloseHandle(pInfo->hThread);
        }
    }
}

int main(int argc, char* argv[])
{
    system("chcp 1251");
    system("cls");

    std::string str, newstr;

    if (argc == 2)
    {
        str = argv[1];
    }
    else
    {
        std::cout << "Не введено дерево процессов!" << std::endl;
        return -1;
    }

    int k = 0;
    for (int i = 0; i < str.size();)
    {
        if (str[i++] == '|')
        {
            while (str[i] != '|')
            {
                prs[k] += str[i++];
            }
            k++;
        }
    }


    int len = 0;
    for (int k = 1; k <= 3; k++)
    {
        len += 2;
        for (int i = 0; i < str.size() - len;)
        {
            if (str[i++] == '|')
            {
                if (str[i + len] == '|')
                {
                    newstr = "";
                    while (str[i] != '|')
                    {
                        newstr += str[i++];
                    }
                    prNames[count++] = newstr;
                }
            }
        }
    }

    STARTUPINFO siParent;
    PROCESS_INFORMATION piParent;
    createNewProcess(&siParent, &piParent, NULL, false, 0);

    std::ofstream file;
    file.open("F0");
    file << "0.(";

    int i = 0;
    while (prNames[i].size() != 4)
    {
        std::ifstream file123;
        file123.open((toFileName(prNames[i])).c_str());
        std::cout << toFileName(prNames[i]).c_str() << std::endl;

        std::string str;
        file123 >> str;

        file << str;
        if (prNames[++i].size() != 4)
        {
            file << ",";
        }
        file123.close();
    }

    file << ")";
    file.close();

    std::cout << "Процесс 0 завершился с результатом result = " << result << std::endl;

    system("pause");
    return 0;
}