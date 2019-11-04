using System;
using System.Collections.Generic;

namespace RDocsDemo.NET.Models
{
    public interface NumberParser
    {
        int transformToDigits(string number);
        string transfromToWords(int number);
    }
}
