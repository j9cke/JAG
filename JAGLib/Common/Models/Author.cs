using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Author
    {
        public int _id { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [RegularExpression(@"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð -]{1,20}", ErrorMessage = "Can only conatins normal name characters")]
        public string _firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [RegularExpression(@"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð -]{1,30}", ErrorMessage = "Can only conatins normal name characters")]
        public string _lastname { get; set; }

        [Required(ErrorMessage = "Birthyear is required.")]
        [RegularExpression(@"\d{4}", ErrorMessage = "Must be a 4 digit year")]
        public string _birthyear { get; set; }
    }
}