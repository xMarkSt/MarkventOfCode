using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles
{
    public abstract class AocPuzzle
    {
        public string PartOne { get; set; }
        public string PartTwo { get; set; }
        public abstract int Year { get; }
        public abstract int Day { get; }
        
        private readonly HttpClient _client;
        private IEnumerable<string> _input;

        protected AocPuzzle()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://adventofcode.com");
            _client.DefaultRequestHeaders.Add("Cookie", $"session={ConfigurationManager.AppSettings.Get("Session")}");
        }

        public void Solve()
        {
            Solve(_input);
        }
        
        public async Task LoadInput()
        {
            string inputDir = $"{AppDomain.CurrentDomain.BaseDirectory}/input/{Year}";
            string filePath = $"{inputDir}/day{Day}.txt";
            if (!Directory.Exists(inputDir))
            {
                Directory.CreateDirectory(inputDir);
            }
            if (!File.Exists(filePath))
            {
                byte[] resp = await _client.GetByteArrayAsync($"{Year}/day/{Day}/input");
                await File.WriteAllBytesAsync(filePath, resp);
            }

            _input = await File.ReadAllLinesAsync(filePath);
        }

        protected abstract void Solve(IEnumerable<string> input);
    }
}