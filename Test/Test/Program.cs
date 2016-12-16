using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace MulticastApp
{
    void IncrementStr(string &str)
    {
        str=" "+str;
        for(int i=str.length()-1; i>=0; i--)
        {
            int n=(str[i]==' ')?0:str[i]-48;
            n++;
            if(n<10)
            {
                str[i]=n+48;
                break;
            }
            else str[i]=n%10+48;
        }a
    }

    class Program
    {

        setlocale(LC_ALL,"Rus");
        cout<<"Число:"; 
        var s = Console.ReadLine();
        IncrementStr(s);

    }

}