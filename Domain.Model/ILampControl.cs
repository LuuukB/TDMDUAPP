using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMDUAPP.Domain.Model
{
    public interface ILampControl
    {
        Task AddLamp(Lamp lamp);
    }
}
