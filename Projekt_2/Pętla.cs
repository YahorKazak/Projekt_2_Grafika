using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2
{
    class Pętla
    {
        
        private E3D e3d;
        public bool start { get; private set; }
        public void Load(E3D e3d)
        {
            this.e3d = e3d;
        }
        public async void Start()
        {

            start = true;

            DateTime Time = DateTime.Now;

            while (start)
            {
                TimeSpan Petla = DateTime.Now - Time;
                Time = Time + Petla;
                e3d.UserUpdate(Petla);
                await Task.Delay(8);
            }
        } 
        public void Stop()
        {
            start = false;
        }
    }
}
