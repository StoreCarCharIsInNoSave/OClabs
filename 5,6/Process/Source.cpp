#include <direct.h>
#include <string>
#include <iostream>
#include <fstream>
#include <conio.h>
#include <cstdlib>
#include <windows.h>
#pragma hdrstop
#include <iostream>


//---------------------------------------------------------------------------

#pragma argsused

using namespace std;

int main(int argc, char* argv[])
{
    std::cout << "Process" << endl;


    wchar_t NameBuffer[MAX_PATH];
    wchar_t SysNameBuffer[MAX_PATH];
    DWORD VSNumber;
    DWORD MCLength;
    DWORD FileSF;

    if (GetVolumeInformation((LPCWSTR)"C:\\", NameBuffer, sizeof(NameBuffer), //Чтобы указать дескриптор при получении этой информации,
                                                                              //используйте функцию GetVolumeInformationByHandleW .
        &VSNumber, &MCLength, &FileSF, SysNameBuffer, sizeof(SysNameBuffer)))
    {
        std::cout << VSNumber;
    }
    else
    {
        std::cout << 0;
    }
    Sleep(2000);
    std::cout << "Process finish" << endl;

    return 0;
}
//---------------------------------------------------------------------------
