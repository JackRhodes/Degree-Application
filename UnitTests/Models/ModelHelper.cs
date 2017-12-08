using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Degree_Application_Test.Models
{
    public class ModelHelper : IModelHelper
    {
        /// <summary>
        /// Checks Model against data annotation validation.
        /// </summary>
        /// <param name="model">Model to check.</param>
        /// <returns>Valid (true) or invalid (false).</returns>
        public bool CheckModelValidation(object model)
        {
            //Create a context for the validation.
            ValidationContext validationContext = new ValidationContext(model, null, null);
            //Create a storage method to store results of the check.
            var result = new List<ValidationResult>();
            //Check whether the AccountModel provided is valid against the context.
            bool valid = Validator.TryValidateObject(model, validationContext, result, true);

            return valid;
        }

        /// <summary>
        /// Converts string to Byte Array
        /// </summary>
        /// <param name="input"> Valid Byte Characters</param>
        /// <returns></returns>
        public byte[] ConvertStingToByteArray(string input)
        {
            //Create a temporary List.
            List<byte> imageStorage = new List<byte>();
            //Loop through each character in the string and convert to a Byte.
            foreach (char x in input)
            {
                //Store each byte within the temporary storage.
                imageStorage.Add(Convert.ToByte(x));

            }

            //Convert List to Byte Array
            return imageStorage.ToArray();

        }
    }
}
