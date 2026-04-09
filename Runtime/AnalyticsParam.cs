using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace Depra.Analytics
{
	public struct Param
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Param String(FixedString64Bytes key, FixedString64Bytes value) => new()
			{ Key = key, StringValue = value, Type = ParamType.STRING };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Param Float(FixedString64Bytes key, float value) => new()
			{ Key = key, FloatValue = value, Type = ParamType.FLOAT };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Param Int(FixedString64Bytes key, int value) => new()
			{ Key = key, IntValue = value, Type = ParamType.INT };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Param Bool(FixedString64Bytes key, bool value) =>
			new() { Key = key, BoolValue = value, Type = ParamType.BOOL };

		public FixedString64Bytes Key;
		public FixedString64Bytes StringValue;

		public float FloatValue;
		public int IntValue;
		public bool BoolValue;

		public ParamType Type;

		public enum ParamType
		{
			STRING,
			FLOAT,
			INT,
			BOOL
		}
	}

	public ref struct Params
	{
		private readonly Span<Param> _buffer;
		private int _count;

		public readonly Span<Param> Items => _buffer[.._count];

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params(Span<Param> buffer)
		{
			_buffer = buffer;
			_count = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params Add(Param param)
		{
			_buffer[_count++] = param;
			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params AddString(FixedString64Bytes key, FixedString64Bytes value) => Add(Param.String(key, value));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params AddFloat(FixedString64Bytes key, float value) => Add(Param.Float(key, value));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params AddInt(FixedString64Bytes key, int value) => Add(Param.Int(key, value));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Params AddBool(FixedString64Bytes key, bool value) => Add(Param.Bool(key, value));

		public readonly void CopyTo(Dictionary<string, object> dict)
		{
			for (var index = 0; index < _count; index++)
			{
				dict[_buffer[index].Key.Value] = _buffer[index].Type switch
				{
					Param.ParamType.STRING => _buffer[index].StringValue,
					Param.ParamType.FLOAT => _buffer[index].FloatValue,
					Param.ParamType.INT => _buffer[index].IntValue,
					Param.ParamType.BOOL => _buffer[index].BoolValue,
					_ => throw new ArgumentOutOfRangeException()
				};
			}
		}
	}
}