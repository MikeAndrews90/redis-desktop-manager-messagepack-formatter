using System;
using MessagePack;
using Newtonsoft.Json;

namespace RedisMessagePackFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "info")
            {
                OutputFormatterInfo();
                return;
            }

            if (args[0] == "decode")
            {
                DecodeMessagePack();
            }
        }

        private static void DecodeMessagePack()
        {
            string input;
            while ((input = Console.ReadLine()) != null)
            {
                var bytes = Convert.FromBase64CharArray(input.ToCharArray(), 0, input.Length);
                var messagePackObject = MessagePackSerializer.Typeless.Deserialize(bytes);
                var prettyJson = JsonConvert.SerializeObject(messagePackObject, Formatting.Indented);
                Console.Write(JsonConvert.SerializeObject(new DecodeResponse(prettyJson)));
            }
        }

        private static void OutputFormatterInfo()
        {
            var info = new {version = "1.0.0", description = "MessagePack Formatter"};
            Console.Write(JsonConvert.SerializeObject(info));
        }
    }

    class DecodeResponse
    {
        public DecodeResponse(string output)
        {
            Output = output;
        }

        [JsonProperty("output")]
        public string Output { get; private set; }
        [JsonProperty("read-only")] 
        public string ReadOnly { get; private set; } = "true";
        [JsonProperty("format")]
        public string Format { get; private set; } = "plain_text";
    }
}
