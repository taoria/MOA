using UnityEngine;

public static class UnityMath {
    public static float Sigmoid(float x) {
        return 1.0f / (1 + Mathf.Exp(-x));
    }
}