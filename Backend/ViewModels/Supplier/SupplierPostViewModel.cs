using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bageri.api.ViewModels;

namespace Backend.ViewModels.Supplier;

public class SupplierPostViewModel 
{
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
