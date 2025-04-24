namespace Application.Common.Exceptions;

using System;

public class NotFoundException(string name, object key) : Exception($"Entity '{name}' ({key}) was not found.")
{
}
