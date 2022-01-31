using System;

public class User
{
	public int service_id;
	public int server_id;
	public int amount;

	public User(int service_id, int server_id, int amount)
	{
		this.service_id = service_id;
		this.server_id = server_id;
		this.amount = amount;
	}
}
