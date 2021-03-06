﻿using System;

namespace ProEvoCanary.Application.EventHandlers.Events.Commands
{
	public class AddEventCommand
	{
		public AddEventCommand(string name, DateTime dateOfEvent)
		{
			Id = Guid.NewGuid();
			Name = name;
			DateOfEvent = dateOfEvent;
		}

		public Guid Id { get; }
		public String Name { get; }
		public DateTime DateOfEvent { get; }
	}
}
