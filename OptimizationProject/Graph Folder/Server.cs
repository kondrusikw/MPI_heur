using System;

public class Server
{
	public int server_id;
	public int cores;
	public int x;
	public int y;

	public Server(int cores, int x, int y)
	{
		this.cores = cores;
		this.x = x;
		this.y = y;
	}
}
