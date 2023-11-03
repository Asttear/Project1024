using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1024.Shared.Responses;

public class FailedResponse
{
    public IEnumerable<string>? Errors { get; set; }
}
