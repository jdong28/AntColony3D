using System;
using System.Collections.Generic;
using UnityEngine;

// Custom keyvaluepair class because default one does not allow
// changing of value inside foreach
public class MovementDirection<Key, Val> {

    public Key Direction { get; set; }
    public Val Probability { get; set; }

    public MovementDirection() { }

    public MovementDirection(Key key, Val val) {
        this.Direction = key;
        this.Probability = val;
    }
}
