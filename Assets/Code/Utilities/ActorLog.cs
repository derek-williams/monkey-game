using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class ActorLog
{
  [TextArea(10, 50)]
  public string AllLogs;
  public List<LogEntry> ActorStateEntries = new List<LogEntry>();
  public List<LogEntry> RagdollEntries = new List<LogEntry>();
  public List<LogEntry> AIStateEntries = new List<LogEntry>();
  public List<LogEntry> CombatEntries = new List<LogEntry>();
  public List<LogEntry> BehaviorTreeEntries = new List<LogEntry>();


  /// <summary>
  /// You do not need to add time or info about type of log, we generate a prefix
  /// </summary>
  /// <param name="type"></param>
  /// <param name="message"></param>
  public void AddToLog(ActorLogType type, string message)
  {
    string formattedMessage = GetLogPrefix(type) + message + "\n\n";
    AllLogs += formattedMessage;

    switch (type)
    {
      case ActorLogType.ActorState:
        ActorStateEntries.Add(new LogEntry(Time.time, formattedMessage));
        break;
      case ActorLogType.Ragdoll:
        RagdollEntries.Add(new LogEntry(Time.time, formattedMessage));
        break;
    }
  }

  public string GetLogPrefix(ActorLogType type)
  {
    string output = "";
    string time = Math.Round((double)Time.time, 2).ToString();

    //pad time so that it lines up for readability
    output += time.PadRight(8) + "| ";

    switch (type)
    {
      case ActorLogType.ActorState:
        output += "ActorState   | ";
        break;
      case ActorLogType.Ragdoll:
        output += "Ragdoll      | ";
        break;
    }

    return output;
  }

  public string GenerateLog(ActorLogType[] types)
  {
    List<LogEntry> allEntries = new List<LogEntry>();

    foreach (ActorLogType type in types)
    {
      switch (type)
      {
        case ActorLogType.ActorState:
          allEntries.AddRange(ActorStateEntries);
          break;
        case ActorLogType.Ragdoll:
          allEntries.AddRange(RagdollEntries);
          break;
      }
    }

    string output = "";

    allEntries.OrderBy(x => x.TimeStamp);

    foreach (LogEntry entry in allEntries)
    {
      output += entry.Message;
    }

    return output;
  }

  public string GenerateLog(ActorLogType type)
  {
    List<LogEntry> allEntries = new List<LogEntry>();

    switch (type)
    {
      case ActorLogType.ActorState:
        allEntries.AddRange(ActorStateEntries);
        break;
      case ActorLogType.Ragdoll:
        allEntries.AddRange(RagdollEntries);
        break;
    }


    string output = "";

    allEntries.OrderBy(x => x.TimeStamp);

    foreach (LogEntry entry in allEntries)
    {
      output += entry.Message;
    }

    return output;
  }
}

public class LogEntry
{
  public float TimeStamp;
  public string Message;

  public LogEntry(float timeStamp, string message)
  {
    TimeStamp = timeStamp;
    Message = message;
  }
}

public enum ActorLogType
{
  ActorState,
  Ragdoll,
}
