using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizationProject.Graph_Folder
{
    public class Graph
    {
       // public List<Edge> Edges { get; set; }
        //public List<Demand> Demands { get; set; }
        public List<Server> Servers {get; set;}
        public List<User> Users {get; set;}
        public List<Service> Services {get; set;}

        public Graph()
        {
           // Edges = new List<Edge>();
          //  Demands = new List<Demand>();
          Servers = new List<Server>();
          Users = new List<User>();
          Services = new List<Service>();
        }

        public Graph (List<Server> Servers, List<User> Users, List<Service> Services){
            this.Servers = Servers;
            this.Users = Users;
            this.Services = Services;
        }
       /* public void CreateEdge(int Id, int Start, int End, int SizeModule, int NumberModules, int CostModule)
        {
            Edges.Add(new Edge(Id, Start, End, SizeModule, NumberModules, CostModule));
        }
        public void CreateDemand(int Start, int End, int volume, List<Path> paths)
        {
            Demands.Add(new Demand(Start, End, volume, paths));
        }*/
        /*public void CreateServers(int cores, int x, int y)
        {
            Servers.Add(new Demand(cores, x, y));
        }
        public void CreateUsers(int service_id, int server_id, int amount)
        {
            Users.Add(new Demand( service_id, server_id, amount));
        }
        public void CreateServices(int cores)
        {
            Services.Add(new Demand(cores));
        }*/
        /*public int getMaxNumberOfPaths()
        {
            int ret = 0;
            foreach (Demand d in Demands)
                if(d.Paths.Count > ret) ret = d.Paths.Count;
            return ret;
        }
        public string PrintDemand(Demand demand)
        {
            return demand.ToString();
        }*/
    }
}
