using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizationProject.Algorithm_Folder
{
    public class Gene
    {
        public int[] services;

        public Gene(Gene gene)
        {
            services = new int[gene.services.Length];
            for (int i = 0; i < gene.services.Length; i++)
            {
                services[i] = gene.services[i];
            }

        }
        public Gene(int[] services)
        {
            this.services = services;
            
        }
        public void mutate(Random random)//zaimplementowane jako losowanie genu, który odda jednostkę wartości innemu genowi  TODO
        {
            /*if (services.Length == 1) return; // nie może mutować
            int i = random.Next(0, services.Length);//gen oddający jednostkę wartości
            while (ValueOnPath[i] == 0)
            {
                i = random.Next(0, ValueOnPath.Length);//losujemy aż nie trafimy na niepusty gen
            }
            int j = random.Next(0, ValueOnPath.Length);//gen otrzymujący jednostkę wartości
            while (i == j)
                j = random.Next(0, ValueOnPath.Length);
            ValueOnPath[i]--;
            ValueOnPath[j]++;*/

            int usedServices = 0;
            foreach (int s in services){
                usedServices += s;
            }
            int i,j;
            i = random.Next(0, services.Length);
            j = i;
            if ( usedServices == 0) {
                //losowanie jakiejs usługi jednej
                services[i] = 1;
            }

            else if (usedServices == services.Length) 
                {
                // losowanie która robi out
                services[i] = 0;
            }

            else {
                //priv metody add i remove service z 50% szansą wystąpienia ich
                while(j == i){
                  j = random.Next(0, services.Length);              
                }

                if(services[i] == 0){
                    services[i] = 1;
                }
                else{
                    services[i] = 0;
                }

                if(services[j] == 0){
                    services[j] = 1;
                }
                else{
                    services[j] = 0;
                }
            }

        }
    }
}
