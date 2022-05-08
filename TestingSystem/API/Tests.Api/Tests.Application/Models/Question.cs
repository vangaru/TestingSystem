﻿namespace Tests.Application.Models;

public class Question
{
    public string? Name { get; set; }
    public string? ExpectedAnswer { get; set; }
    public QuestionType Type { get; set; }
    public IEnumerable<string>? SelectableQuestionNames { get; set; }
}