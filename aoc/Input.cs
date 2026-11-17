using System.Collections.Immutable;

namespace aoc
{
	public static partial class Input
	{
		public const string DEFAULT_INPUT_FILENAME = "input.txt";
		public const string DEFAULT_SAMPLE_INPUT_FILENAME = "sample.txt";
		
		//
		// Read... from file
		//
		public static ImmutableList<char> ReadCharList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllText(fileName)];
		public static ImmutableList<string> ReadStringList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllLines(fileName)];
		public static ImmutableList<int> ReadIntList(string fileName = DEFAULT_INPUT_FILENAME) => [.. ReadStringList(fileName).Select(int.Parse)];
		public static ImmutableList<ImmutableList<int>> ReadIntListList(string fileName = DEFAULT_INPUT_FILENAME) =>
			[.. ReadStringList(fileName).Select(s => s.Split(' ').Select(int.Parse).ToImmutableList()).ToImmutableList()];
		public static ImmutableList<(string s1, string s2)> ReadStringStringList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0], tokens[1]));
		
		public static ImmutableList<(char c, int i)> ReadCharIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0][0], int.Parse(tokens[1])));
		
		public static IEnumerable<(string s, int i)> ReadStringIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0], int.Parse(tokens[1])));
		public static ImmutableList<(int i1, int i2)> ReadIntIntList(string fileName = DEFAULT_INPUT_FILENAME, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseIntIntList(File.ReadAllText(fileName), stringDelimiter, intDelimiter);
		public static ImmutableList<(long l1, long l2)> ReadLongLongList(string fileName = DEFAULT_INPUT_FILENAME, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseLongLongList(File.ReadAllText(fileName), stringDelimiter, intDelimiter);

		//
		// Parse ... from string
		//
		public static ImmutableList<string> ParseStringList(string text, string delimiter = " ") => [.. text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)];
		public static ImmutableList<int> ParseIntList(string text, string delimiter = " ") => [.. ParseStringList(text, delimiter).Select(int.Parse)];
		public static ImmutableList<long> ParseLongList(string text, string delimiter = " ") => [.. ParseStringList(text, delimiter).Select(long.Parse)];
		public static ImmutableList<(int i1, int i2)> ParseIntIntList(string text, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseTokenized(ParseStringList(text, stringDelimiter), tokens => (int.Parse(tokens[0]), int.Parse(tokens[1])), intDelimiter);
		public static ImmutableList<(long l1, long l2)> ParseLongLongList(string text, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseTokenized(ParseStringList(text, stringDelimiter), tokens => (long.Parse(tokens[0]), long.Parse(tokens[1])), intDelimiter);

		//
		// Helpers
		//		
		private static ImmutableList<T> ReadTokenized<T>(string fileName, Func<string[], T> tokenParser) =>
			[.. ParseTokenized(ReadStringList(fileName), tokenParser, " ")];
		private static ImmutableList<T> ParseTokenized<T>(ImmutableList<string> tokens, Func<string[], T> tokenParser, string delimiter) =>
			[.. tokens.Select(l => tokenParser(l.Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)))];
	}
}
