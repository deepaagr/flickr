using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearchWPF.API.Interface
{
    public interface IResult
    {
         bool IsSuccessful { get; set; }
         string ErrorMessage { get; set; }
    }
}
