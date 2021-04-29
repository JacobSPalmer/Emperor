using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class User : IEquatable<User>
{
    public string UserName { get; set; }
    public int UserScore { get; set; }
    public string UserTime { get; set; }
    public int UserMoves { get; set; }

    public override string ToString()
    {
        return "Player: " + UserName + ", Score: " + UserScore + ", Time: " + UserTime + ", Moves: " + UserMoves;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        User objAsUser = obj as User;
        if (objAsUser == null) return false;
            else return Equals(objAsUser);
    }

    public override int GetHashCode()
    {
        return this.UserName.GetHashCode() * 17;
    }

    public bool Equals(User other)
    {
            if (other == null) return false;
            return (this.UserName.Equals(other.UserName));
    }
}
