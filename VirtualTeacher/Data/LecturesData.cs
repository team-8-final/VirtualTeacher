using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Data;

public static class LecturesData
{
    public static List<Lecture> Seed()
    {
        List < Lecture> lectures = new List<Lecture>
        {
            new() { Id = 1, TeacherId = 2, CourseId = 1, Title = "Basic Grammar", Description = "Learn the foundational grammar necessary for daily conversations.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = GenerateAssignmentLink(1,1)},
            new() { Id = 2, TeacherId = 2, CourseId = 1, Title = "Common Vocabulary", Description = "Expand your English vocabulary with everyday words.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = GenerateAssignmentLink(1,2)},
            new() { Id = 3, TeacherId = 2, CourseId = 1, Title = "Simple Sentences", Description = "Construct simple sentences for effective communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 4, TeacherId = 2, CourseId = 1, Title = "Conversational Phrases", Description = "Master phrases that make conversations flow smoothly.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 5, TeacherId = 2, CourseId = 1, Title = "Pronunciation Practice", Description = "Improve your English pronunciation and sound more natural.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 2: Conversational English
            new() { Id = 6, TeacherId = 3, CourseId = 2, Title = "Everyday Conversations", Description = "Engage in English conversations with confidence in daily situations.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 7, TeacherId = 3, CourseId = 2, Title = "Listening Skills", Description = "Sharpen your listening skills to understand English better.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 8, TeacherId = 3, CourseId = 2, Title = "Speaking Fluently", Description = "Techniques to improve your fluency and speak English more fluidly.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 9, TeacherId = 3, CourseId = 2, Title = "Idioms and Phrases", Description = "Learn idioms and phrases to enrich your conversational English.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 3: Business English Essentials
            new() { Id = 10, TeacherId = 4, CourseId = 3, Title = "Professional Emails", Description = "Craft professional emails for effective business communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 11, TeacherId = 4, CourseId = 3, Title = "Presentation Skills", Description = "Develop skills to deliver impactful presentations in English.", VideoLink = "https://www.youtube.com/embed/YGYdov1XE0gg", AssignmentLink = ""},
            new() { Id = 12, TeacherId = 4, CourseId = 3, Title = "Business Negotiations", Description = "Navigate English negotiations with confidence and strategy.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 13, TeacherId = 4, CourseId = 3, Title = "Corporate Communication", Description = "Enhance your communication skills in a corporate environment.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 4: English Grammar in Depth
            new() { Id = 14, TeacherId = 5, CourseId = 4, Title = "Complex Sentences", Description = "Explore the structure and usage of complex sentences in English.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 15, TeacherId = 5, CourseId = 4, Title = "Verb Tenses", Description = "A deep dive into English verb tenses and their applications.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 16, TeacherId = 5, CourseId = 4, Title = "Passive Voice", Description = "Understanding the passive voice and when to use it in English.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 17, TeacherId = 5, CourseId = 4, Title = "Modal Verbs", Description = "Learn about modal verbs and their various uses in English grammar.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 18, TeacherId = 5, CourseId = 4, Title = "Conditional Sentences", Description = "Master the conditional sentences to express possibilities and hypothetical situations.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 5: Advanced English Composition
            new() { Id = 19, TeacherId = 6, CourseId = 5, Title = "Essay Writing", Description = "Techniques and strategies for writing compelling essays in English.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 20, TeacherId = 6, CourseId = 5, Title = "Creative Writing", Description = "Unlock your creativity and learn to write captivating stories and narratives.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 21, TeacherId = 6, CourseId = 5, Title = "Research Papers", Description = "Guidance on crafting research papers, from thesis development to conclusion.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 22, TeacherId = 6, CourseId = 5, Title = "Technical Writing", Description = "Learn the principles of technical writing for clear and effective communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 23, TeacherId = 6, CourseId = 5, Title = "Persuasive Writing", Description = "Master the art of persuasive writing to influence and engage your audience.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 6: German Basics
            new() { Id = 24, TeacherId = 7, CourseId = 6, Title = "Introduction to German", Description = "Start your journey with the basics of the German language.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 25, TeacherId = 7, CourseId = 6, Title = "German Pronunciation", Description = "Learn the fundamentals of German sounds and pronunciation.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 26, TeacherId = 7, CourseId = 6, Title = "Basic Grammar", Description = "Understand the structure of the German language with basic grammar.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 27, TeacherId = 7, CourseId = 6, Title = "Essential Vocabulary", Description = "Build your German vocabulary with essential words and phrases.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 7: Everyday German
            new() { Id = 28, TeacherId = 8, CourseId = 7, Title = "Daily Conversations", Description = "Improve your conversational skills for everyday situations in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 29, TeacherId = 8, CourseId = 7, Title = "Shopping in German", Description = "Learn useful phrases for shopping and retail interactions in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 30, TeacherId = 8, CourseId = 7, Title = "Eating Out", Description = "Navigate dining out in German-speaking countries with ease.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 31, TeacherId = 8, CourseId = 7, Title = "Getting Around", Description = "Phrases and vocabulary for transportation and getting around in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 8: German for Business
            new() { Id = 32, TeacherId = 9, CourseId = 8, Title = "Business Communication", Description = "Learn the essentials of German business communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 33, TeacherId = 9, CourseId = 8, Title = "Writing Business Emails", Description = "Master the format and etiquette of business email writing in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 34, TeacherId = 9, CourseId = 8, Title = "Presenting in German", Description = "Skills and strategies for delivering effective presentations in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 35, TeacherId = 9, CourseId = 8, Title = "Professional Networking", Description = "Expand your professional network with key German phrases and etiquette.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 9: Intermediate German Grammar
            new() { Id = 36, TeacherId = 10, CourseId = 9, Title = "Reflexive Verbs", Description = "Understand and use reflexive verbs in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 37, TeacherId = 10, CourseId = 9, Title = "Subjunctive Mood", Description = "Explore the usage of the subjunctive mood for hypotheticals.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 38, TeacherId = 10, CourseId = 9, Title = "Relative Clauses", Description = "Learn how to construct relative clauses in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 39, TeacherId = 10, CourseId = 9, Title = "Dative Case", Description = "Master the dative case and its uses in German sentences.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 40, TeacherId = 10, CourseId = 9, Title = "Adjective Endings", Description = "Understand how adjective endings change in German grammar.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 41, TeacherId = 10, CourseId = 9, Title = "Prepositions and Cases", Description = "Dive into German prepositions and their associated cases.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 42, TeacherId = 10, CourseId = 9, Title = "Conjunctions", Description = "Learn about coordinating and subordinating conjunctions in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 43, TeacherId = 10, CourseId = 9, Title = "Sentence Structure", Description = "Advanced sentence structure techniques for clarity and fluency.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 10: Advanced German: Language & Culture
            new() { Id = 44, TeacherId = 2, CourseId = 10, Title = "German Dialects", Description = "Explore the variety and richness of German dialects.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = GenerateAssignmentLink(10,44)},
            new() { Id = 45, TeacherId = 2, CourseId = 10, Title = "German Literature", Description = "An introduction to classic and contemporary German literature.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 46, TeacherId = 2, CourseId = 10, Title = "German History and Culture", Description = "Understand the historical events that shaped Germany and its culture.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 47, TeacherId = 2, CourseId = 10, Title = "Formal and Informal Speech", Description = "Nuances of formal and informal speech in German.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 48, TeacherId = 2, CourseId = 10, Title = "German Media", Description = "Analyzing German media to understand contemporary issues.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 49, TeacherId = 2, CourseId = 10, Title = "German Film", Description = "Explore German cinema and its impact on culture and society.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 50, TeacherId = 2, CourseId = 10, Title = "Business German", Description = "Advanced communication skills for professional settings.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 11: Introduction to Japanese
            new() { Id = 51, TeacherId = 3, CourseId = 11, Title = "Hiragana and Katakana", Description = "Learn the basics of Japanese writing systems: Hiragana and Katakana.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 52, TeacherId = 3, CourseId = 11, Title = "Basic Grammar", Description = "Introduction to Japanese grammar structures and sentence patterns.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 53, TeacherId = 3, CourseId = 11, Title = "Essential Vocabulary", Description = "Build your foundational Japanese vocabulary for daily use.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 54, TeacherId = 3, CourseId = 11, Title = "Greetings and Introductions", Description = "Master common greetings and introduce yourself in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 55, TeacherId = 3, CourseId = 11, Title = "Numbers and Time", Description = "Learn to count in Japanese and tell time.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 56, TeacherId = 3, CourseId = 11, Title = "Everyday Phrases", Description = "Useful phrases for everyday scenarios in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 57, TeacherId = 3, CourseId = 11, Title = "Japanese Culture", Description = "Insights into Japanese culture and etiquette.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 12: Conversational Japanese
            new() { Id = 58, TeacherId = 4, CourseId = 12, Title = "Basic Conversation Skills", Description = "Develop your conversational skills for everyday communication in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 59, TeacherId = 4, CourseId = 12, Title = "Asking Questions", Description = "Learn how to ask questions in various contexts in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 60, TeacherId = 4, CourseId = 12, Title = "Describing Your Daily Routine", Description = "Express your daily activities and routine in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 61, TeacherId = 4, CourseId = 12, Title = "Shopping and Dining", Description = "Conversational Japanese for shopping and dining out.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 62, TeacherId = 4, CourseId = 12, Title = "Travel Phrases", Description = "Essential phrases for traveling and navigating in Japan.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 63, TeacherId = 4, CourseId = 12, Title = "Cultural Nuances in Conversation", Description = "Understanding cultural nuances to enhance your conversational Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 13: Japanese for Business
            new() { Id = 64, TeacherId = 5, CourseId = 13, Title = "Business Etiquette", Description = "Master the essentials of Japanese business etiquette.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 65, TeacherId = 5, CourseId = 13, Title = "Keigo: Honorific Language", Description = "Learn the use of keigo to communicate respectfully in business settings.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 66, TeacherId = 5, CourseId = 13, Title = "Business Vocabulary", Description = "Expand your business-related Japanese vocabulary.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 67, TeacherId = 5, CourseId = 13, Title = "Writing Business Emails", Description = "Learn the format and etiquette of Japanese business correspondence.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 68, TeacherId = 5, CourseId = 13, Title = "Presenting in Japanese", Description = "Skills and strategies for effective business presentations in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 69, TeacherId = 5, CourseId = 13, Title = "Negotiation Techniques", Description = "Techniques for successful negotiation in Japanese business contexts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 70, TeacherId = 5, CourseId = 13, Title = "Networking in Japan", Description = "Build and maintain professional relationships in Japan.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 14: Intermediate Japanese Writing
            new() { Id = 71, TeacherId = 6, CourseId = 14, Title = "Kanji Foundations", Description = "Strengthen your kanji knowledge with essential characters and readings.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 72, TeacherId = 6, CourseId = 14, Title = "Kana in Context", Description = "Effective use of hiragana and katakana in various writing contexts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 73, TeacherId = 6, CourseId = 14, Title = "Sentence Structure", Description = "Advanced techniques for constructing clear and nuanced sentences in Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 74, TeacherId = 6, CourseId = 14, Title = "Writing Styles", Description = "Explore different writing styles, from formal to colloquial Japanese.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 75, TeacherId = 6, CourseId = 14, Title = "Practical Writing Tasks", Description = "Apply your skills to practical writing tasks like letters and diaries.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 76, TeacherId = 6, CourseId = 14, Title = "Creative Writing", Description = "Unleash your creativity in Japanese through short stories and poetry.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 15: Advanced Japanese: Language & Literature
            new() { Id = 77, TeacherId = 7, CourseId = 15, Title = "Classical Japanese", Description = "An introduction to classical Japanese language and its literary beauty.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 78, TeacherId = 7, CourseId = 15, Title = "Modern Japanese Literature", Description = "Explore the evolution of Japanese literature from the Meiji era to the present.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 79, TeacherId = 7, CourseId = 15, Title = "Literary Analysis", Description = "Techniques for analyzing Japanese texts, from structure to thematic elements.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 80, TeacherId = 7, CourseId = 15, Title = "Advanced Reading Practices", Description = "Deepen your reading comprehension with advanced texts and novels.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 81, TeacherId = 7, CourseId = 15, Title = "Writing Critiques", Description = "Learn to write insightful critiques of Japanese literary works.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 82, TeacherId = 7, CourseId = 15, Title = "Fluency Development", Description = "Advanced strategies for achieving fluency in Japanese, focusing on both speaking and writing.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 16: French for Beginners
            new() { Id = 83, TeacherId = 8, CourseId = 16, Title = "Introduction to French", Description = "Begin your French journey with the basics of pronunciation and greetings.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 84, TeacherId = 8, CourseId = 16, Title = "Basic Grammar", Description = "Learn the foundational grammar rules of French language.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 85, TeacherId = 8, CourseId = 16, Title = "Essential Vocabulary", Description = "Expand your French vocabulary with essential words and phrases.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 86, TeacherId = 8, CourseId = 16, Title = "Numbers and Dates", Description = "Understand how to use numbers and tell dates in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 87, TeacherId = 8, CourseId = 16, Title = "Everyday Conversations", Description = "Practice common conversational phrases used in daily situations.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 17: Practical French Conversations
            new() { Id = 88, TeacherId = 9, CourseId = 17, Title = "Meeting and Greeting", Description = "Learn how to introduce yourself and meet others in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 89, TeacherId = 9, CourseId = 17, Title = "Shopping in French", Description = "Gain the language skills to shop confidently in French-speaking countries.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 90, TeacherId = 9, CourseId = 17, Title = "Ordering Food", Description = "Master the phrases needed to order food in French cafes and restaurants.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 91, TeacherId = 9, CourseId = 17, Title = "Asking for Directions", Description = "Learn how to ask for and understand directions in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 92, TeacherId = 9, CourseId = 17, Title = "Traveling in French", Description = "Useful phrases for traveling, including airports, train stations, and hotels.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 18: Business French
            new() { Id = 93, TeacherId = 10, CourseId = 18, Title = "Professional Introductions", Description = "Formal introductions and greetings in a business context.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 94, TeacherId = 10, CourseId = 18, Title = "Writing Business Emails", Description = "Key phrases and formats for writing professional emails in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 95, TeacherId = 10, CourseId = 18, Title = "Negotiating in French", Description = "Language and strategies for effective negotiation in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 96, TeacherId = 10, CourseId = 18, Title = "Business Vocabulary", Description = "Expand your industry-specific vocabulary for business in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 97, TeacherId = 10, CourseId = 18, Title = "Presentations in French", Description = "Craft and deliver compelling business presentations in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 19: Intermediate French Grammar
            new() { Id = 98, TeacherId =  2, CourseId = 19, Title = "Advanced Verb Tenses", Description = "Dive deeper into French verb tenses, including the subjunctive and conditional.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 99, TeacherId =  2, CourseId = 19, Title = "Complex Sentences", Description = "Construct and understand complex sentence structures in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 100, TeacherId = 2, CourseId = 19, Title = "Pronominal Verbs", Description = "Master the use of reflexive and pronominal verbs in French.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 101, TeacherId = 2, CourseId = 19, Title = "Adjective Agreement", Description = "Rules and nuances of adjective agreement in gender and number.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 102, TeacherId = 2, CourseId = 19, Title = "Using Prepositions", Description = "Explore the use of prepositions in French to express location, time, and more.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 20: French Literature and Culture
            new() { Id = 103, TeacherId = 3, CourseId = 20, Title = "Introduction to French Literature", Description = "Discover the world of French literature from medieval to modern times.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 104, TeacherId = 3, CourseId = 20, Title = "French Poetry", Description = "Explore the beauty and depth of French poetry.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 105, TeacherId = 3, CourseId = 20, Title = "Contemporary French Authors", Description = "Get to know the works and influence of contemporary French authors.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 106, TeacherId = 3, CourseId = 20, Title = "French Cinema", Description = "An overview of French cinema and its impact on culture and society.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 107, TeacherId = 3, CourseId = 20, Title = "Cultural Icons", Description = "Learn about French cultural icons and their significance.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 21: Spanish for Absolute Beginners
            new() { Id = 108, TeacherId = 4, CourseId = 21, Title = "Getting Started with Spanish", Description = "An introduction to Spanish pronunciation and basic phrases.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 109, TeacherId = 4, CourseId = 21, Title = "Basic Spanish Grammar", Description = "Foundational grammar concepts for building sentences in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 110, TeacherId = 4, CourseId = 21, Title = "Essential Vocabulary", Description = "Key vocabulary for everyday situations and simple conversations.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 111, TeacherId = 4, CourseId = 21, Title = "Conversational Phrases", Description = "Practical phrases to help you start speaking Spanish quickly.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 112, TeacherId = 4, CourseId = 21, Title = "Numbers, Colors, and Time", Description = "Learn to discuss numbers, colors, and tell time in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 22: Daily Spanish Speaking
            new() { Id = 113, TeacherId = 5, CourseId = 22, Title = "Conversational Practice", Description = "Enhance your conversational skills for daily interactions in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 114, TeacherId = 5, CourseId = 22, Title = "Navigating Daily Situations", Description = "Learn Spanish phrases for navigating daily situations like shopping and public transportation.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 115, TeacherId = 5, CourseId = 22, Title = "Cultural Insights", Description = "Gain insights into Spanish-speaking cultures to enhance your communication skills.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 116, TeacherId = 5, CourseId = 22, Title = "Language for Travelers", Description = "Essential Spanish language skills for travelers and expats.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 117, TeacherId = 5, CourseId = 22, Title = "Handling Emergencies", Description = "Important phrases and vocabulary for handling emergencies in Spanish-speaking countries.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 23: Professional Spanish
            new() { Id = 118, TeacherId = 6, CourseId = 23, Title = "Business Communication", Description = "Develop your professional Spanish communication skills for the workplace.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 119, TeacherId = 6, CourseId = 23, Title = "Formal Emails and Correspondence", Description = "Learn how to write formal emails and business correspondence in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 120, TeacherId = 6, CourseId = 23, Title = "Presentations in Spanish", Description = "Key phrases and strategies for delivering effective presentations in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 121, TeacherId = 6, CourseId = 23, Title = "Industry-Specific Vocabulary", Description = "Expand your vocabulary with Spanish terms specific to your industry.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 122, TeacherId = 6, CourseId = 23, Title = "Networking in Spanish", Description = "Language skills for networking and building professional relationships in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 24: Intermediate Spanish Grammar
            new() { Id = 123, TeacherId = 7, CourseId = 24, Title = "Advanced Verb Forms", Description = "Master the use of advanced Spanish verb tenses and moods.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 124, TeacherId = 7, CourseId = 24, Title = "Complex Sentence Construction", Description = "Learn to construct and understand complex sentence structures in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 125, TeacherId = 7, CourseId = 24, Title = "Subjunctive Mood", Description = "Usage and practice of the subjunctive mood in various contexts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 126, TeacherId = 7, CourseId = 24, Title = "Prepositions and Pronouns", Description = "Deep dive into the use of Spanish prepositions and pronouns.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 127, TeacherId = 7, CourseId = 24, Title = "Idiomatic Expressions", Description = "Learn common idiomatic expressions to enhance your fluency in Spanish.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 25: Spanish Culture and Conversation
            new() { Id = 128, TeacherId = 8, CourseId = 25, Title = "Exploring Spanish Culture", Description = "Discover the rich cultural heritage of Spanish-speaking countries.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 129, TeacherId = 8, CourseId = 25, Title = "Conversational Fluency", Description = "Practice and improve your conversational fluency with advanced dialogue.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 130, TeacherId = 8, CourseId = 25, Title = "Regional Dialects", Description = "Learn about the diversity of dialects within the Spanish-speaking world.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 131, TeacherId = 8, CourseId = 25, Title = "Spanish Literature", Description = "An introduction to significant works of Spanish literature.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 132, TeacherId = 8, CourseId = 25, Title = "Cinema and Media", Description = "Explore Spanish cinema and media to understand contemporary issues.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 26: Russian Fundamentals
            new() { Id = 133, TeacherId = 9, CourseId = 26, Title = "Russian Alphabet and Phonetics", Description = "Start with the basics of the Cyrillic alphabet and Russian pronunciation.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 134, TeacherId = 9, CourseId = 26, Title = "Basic Grammar and Vocabulary", Description = "Learn the foundational grammar rules and essential vocabulary of Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 135, TeacherId = 9, CourseId = 26, Title = "Simple Sentences", Description = "Construct simple sentences to start communicating in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 136, TeacherId = 9, CourseId = 26, Title = "Numbers and Time", Description = "Understand how to discuss numbers, dates, and time in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 137, TeacherId = 9, CourseId = 26, Title = "Everyday Conversations", Description = "Practical phrases and vocabulary for daily life interactions in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 27: Conversational Russian for Beginners
            new() { Id = 138, TeacherId = 10, CourseId = 27, Title = "Greetings and Introductions", Description = "Master basic greetings and how to introduce yourself in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 139, TeacherId = 10, CourseId = 27, Title = "Shopping and Eating Out", Description = "Language skills for shopping and dining out in Russian-speaking environments.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 140, TeacherId = 10, CourseId = 27, Title = "Asking Directions", Description = "How to ask for and understand directions in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 141, TeacherId = 10, CourseId = 27, Title = "Basic Conversational Expressions", Description = "Learn key expressions to help you start conversations in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 142, TeacherId = 10, CourseId = 27, Title = "Cultural Do's and Don'ts", Description = "Understand cultural nuances to communicate respectfully in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 28: Russian for Business Communication
            new() { Id = 143, TeacherId = 2, CourseId = 28, Title = "Business Language Fundamentals", Description = "Essential Russian phrases and vocabulary for the business context.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 144, TeacherId = 2, CourseId = 28, Title = "Formal Correspondence", Description = "Writing formal emails and letters in Russian for business purposes.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 145, TeacherId = 2, CourseId = 28, Title = "Negotiations and Meetings", Description = "Language and etiquette for conducting meetings and negotiations in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 146, TeacherId = 2, CourseId = 28, Title = "Business Vocabulary Expansion", Description = "Expand your business-related Russian vocabulary.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 147, TeacherId = 2, CourseId = 28, Title = "Understanding Russian Business Culture", Description = "Gain insights into the Russian business culture and practices.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 29: Intermediate Russian: Grammar and Vocabulary
            new() { Id = 148, TeacherId = 3, CourseId = 29, Title = "Advanced Grammar Concepts", Description = "Explore complex grammatical structures for clearer communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 149, TeacherId = 3, CourseId = 29, Title = "Verb Aspect and Motion Verbs", Description = "Understanding the aspect of verbs and motion verbs in Russian.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 150, TeacherId = 3, CourseId = 29, Title = "Expanding Vocabulary", Description = "Significantly expand your Russian vocabulary with thematic words.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 151, TeacherId = 3, CourseId = 29, Title = "Russian Idiomatic Expressions", Description = "Learn common idiomatic expressions for more natural Russian communication.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 152, TeacherId = 3, CourseId = 29, Title = "Reading and Comprehension", Description = "Enhance your reading skills and comprehension of Russian texts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},

            // Lectures for Course 30: Advanced Russian: Culture, and Literature
            new() { Id = 153, TeacherId = 4, CourseId = 30, Title = "Russian Literature", Description = "Dive into the works of great Russian authors and poets.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 154, TeacherId = 4, CourseId = 30, Title = "Cultural Studies", Description = "Explore the rich cultural heritage and traditions of Russia.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 155, TeacherId = 4, CourseId = 30, Title = "Advanced Conversational Practice", Description = "Practice advanced conversational Russian in a variety of contexts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 156, TeacherId = 4, CourseId = 30, Title = "Russian Cinema and Media", Description = "Analyze Russian cinema and media to understand contemporary societal issues.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
            new() { Id = 157, TeacherId = 4, CourseId = 30, Title = "Analyzing Russian Texts", Description = "Learn techniques for analyzing Russian literary and non-literary texts.", VideoLink = "https://www.youtube.com/embed/e6FKOPlZmYk", AssignmentLink = ""},
        };



        static string GenerateAssignmentLink(int courseId, int lectureId)
        {
            var privateRoot = Path.Combine(Directory.GetCurrentDirectory(), "PrivateData");
            var assignmentDirectory = Path.Combine(privateRoot, "Assignments", "course-" + courseId);

            if (!Directory.Exists(assignmentDirectory))
            {
                Directory.CreateDirectory(assignmentDirectory);
            }

            var fileNameWithoutExtension = "lecture-" + lectureId;
            var existingFiles = Directory.GetFiles(assignmentDirectory, fileNameWithoutExtension + ".*");

            var fileExtension = ".txt";
            var fullPath = Path.Combine(assignmentDirectory, fileNameWithoutExtension + fileExtension);

            return fullPath;
        }




        return lectures;

    }
}