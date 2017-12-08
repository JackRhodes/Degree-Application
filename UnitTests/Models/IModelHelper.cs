using System;
using System.Collections.Generic;
using System.Text;

namespace Degree_Application_Test.Models
{
    interface IModelHelper
    {
        bool CheckModelValidation(object model);

        Byte[] ConvertStingToByteArray(string input);

    }
}
