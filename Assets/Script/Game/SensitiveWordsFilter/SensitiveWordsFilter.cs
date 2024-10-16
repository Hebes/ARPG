using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 敏感词过滤器
/// </summary>
public class SensitiveWordsFilter
{
    public SensitiveWordsFilter()
    {
        this._sensitiveWords = new List<string>();
        TextAsset textAsset = Asset.LoadFromResources<TextAsset>("Conf", "sensitive-words");
        using (StringReader stringReader = new StringReader(textAsset.text))
        {
            string item;
            while ((item = stringReader.ReadLine()) != null)
            {
                this._sensitiveWords.Add(item);
            }
        }
    }

    public bool Filter(string word, out string filteredWord)
    {
        if (string.IsNullOrEmpty(word))
        {
            filteredWord = string.Empty;
            return false;
        }
        bool result = false;
        foreach (string text in this._sensitiveWords)
        {
            if (word.Contains(text))
            {
                word = word.Replace(text, new string('*', text.Length));
                result = true;
            }
        }
        filteredWord = word;
        return result;
    }

    private List<string> _sensitiveWords;
}