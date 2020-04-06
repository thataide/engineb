using System;
using System.Globalization;
using Azure.AI.TextAnalytics;

namespace TextAnalyticsConsole
{
    class Program
    {
        //Docs page for reference:
        //https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/quickstarts/text-analytics-sdk?tabs=version-3&pivots=programming-language-csharp#key-phrase-extraction
        
        
        //Azure parameters:
        private static readonly TextAnalyticsApiKeyCredential credentials = new TextAnalyticsApiKeyCredential("378a9dfddcb240078c3aa43facbb1309");
        private static readonly Uri endpoint = new Uri("https://enginebtextanalytics.cognitiveservices.azure.com/");


        static void Main(string[] args)
        {
            //string inputText = "Insurance policy for SSN on file 123-12-1234 is here by approved. I had a wonderful trip to Seattle last week. My cat might need to see a veterinarian.";
            string inputText = "Form W-4 Department of the Treasury Internal Revenue Service Employee’s Withholding Allowance Certificate a Whether you’re entitled to claim a certain number of allowances or exemption from withholding is subject to review by the IRS. Your employer may be required to send a copy of this form to the IRS. OMB No. 1545-0074 2019 Your first name and middle initial: Thiago Last name: Peres Ataide Home address (number and street or rural route): 160 Riverside Blvd Apt 10L City or town, state, and ZIP code: New York, NY 10069-0707 2 Your social security number: 844605789 3 Single Married Married, but withhold at higher Single rate Note: If married filing separately, check “Married, but withhold at higher Single rate.” 4 If your last name differs from that shown on your social security card, check here. You must call 800-772-1213 for a replacement card. a 5 Total number of allowances you’re claiming (from the applicable worksheet on the following pages) . . . . 5 6 Additional amount, if any, you want withheld from each paycheck . . . . . . . . . . . . . . 6 $ 7 I claim exemption from withholding for 2019, and I certify that I meet both of the following conditions for exemption. Last year I had a right to a refund of all federal income tax withheld because I had no tax liability, and This year I expect a refund of all federal income tax withheld because I expect to have no tax liability. If you meet both conditions, write “Exempt” here . . . . . . . . . . . . . . . a 7 Under penalties of perjury, I declare that I have examined this certificate and, to the best of my knowledge and belief, it is true, correct, and complete. Employee’s signature (This form is not valid unless you sign it.) a Date a 8 Employer’s name and address (Employer: Complete boxes 8 and 10 if sending to IRS and complete boxes 8, 9, and 10 if sending to State Directory of New Hires.) 9 First date of employment 10 Employer identification number (EIN) 2/12/2018 Electronically Signed on 03/07/2019 08:44 AM ET 03/07/2019 91-1144442 Thiago 846605659 Microsoft Corporation, 11 Times Sq Fl 9, New York, NY 10036-6619";

            var client = new TextAnalyticsClient(endpoint, credentials);
            
            EntityRecognition(client, inputText);
            EntityPII(client, inputText);
            KeyPhraseExtraction(client, inputText);
            //SentimentAnalysis
            //LanguageDetection
            //EntityLinking

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }


        static void EntityRecognition(TextAnalyticsClient client, string inputText)
        {
            //"I had a wonderful trip to Seattle last week."
            var response = client.RecognizeEntities(inputText);
            Console.WriteLine("Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                Console.WriteLine($"\t\tLength: {entity.GraphemeLength},\tScore: {entity.ConfidenceScore:F2}\n");
            }
            Console.WriteLine("\n\n");
        }


        static void EntityPII(TextAnalyticsClient client, string inputText)
        {
            //"Insurance policy for SSN on file 123-12-1234 is here by approved."
            var response = client.RecognizePiiEntities(inputText);
            Console.WriteLine("Personally Identifiable Information Entities:");
            foreach (var entity in response.Value)
            {
                Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                Console.WriteLine($"\t\tLength: {entity.GraphemeLength},\tScore: {entity.ConfidenceScore:F2}\n");
            }
            Console.WriteLine("\n\n");
        }


        static void KeyPhraseExtraction(TextAnalyticsClient client, string inputText)
        {
            //"My cat might need to see a veterinarian."
            var response = client.ExtractKeyPhrases(inputText);
            Console.WriteLine("Key phrases:");
            foreach (string keyphrase in response.Value)
            {
                Console.WriteLine($"\t{keyphrase}");
            }
            Console.WriteLine("\n\n");
        }
    }
}
