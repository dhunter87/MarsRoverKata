
using System;
namespace MarsRover.Interfaces
{
	public interface IPosition : ICoordinate
	{
        char Bearing { get; set; }
    }
}

