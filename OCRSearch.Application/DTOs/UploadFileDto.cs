﻿namespace OCRSearch.Application.DTOs;

public class UploadFileDto
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public Stream Content { get; set; }
}