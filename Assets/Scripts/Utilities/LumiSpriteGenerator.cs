using UnityEngine;
using System.Collections;
using System.IO;

public class LumiSpriteGenerator : MonoBehaviour
{
    [Header("Cute Fox Generator - Simple Cartoon Style")]
    public int spriteSize = 512;
    
    [Header("Cute Fox Colors - Simple & Bright")]
    public Color bodyColor = new Color(1f, 0.6f, 0.2f);        // Bright Orange #FF9933
    public Color whiteColor = new Color(1f, 1f, 1f);           // Pure White #FFFFFF
    public Color darkOrangeColor = new Color(0.8f, 0.35f, 0.15f); // Dark Orange #CC5926
    public Color outlineColor = new Color(0f, 0f, 0f);         // Black #000000
    public Color eyeColor = new Color(0f, 0f, 0f);             // Black #000000
    public Color tongueColor = new Color(1f, 0.4f, 0.6f);      // Pink #FF6699
    public Color blushColor = new Color(1f, 0.75f, 0.8f);      // Light Pink #FFBFCC
    
    [Header("Animation Frame Counts")]
    public int idleFrames = 6;
    public int happyFrames = 8;
    public int walkFrames = 8;
    public int jumpFrames = 6;
    public int celebrateFrames = 10;
    public int thinkingFrames = 8;
    public int encourageFrames = 6;
    public int sadFrames = 6;
    public int sleepingFrames = 8;
    public int talkingFrames = 6;
    
    [Header("Cute Proportions")]
    public float headSize = 180f;      // Large head for cuteness
    public float bodySize = 120f;      // Smaller body
    public float eyeSize = 25f;        // Large expressive eyes
    public float outlineWidth = 3f;    // Clean outline thickness
    
    [ContextMenu("🎨 GENERATE ENHANCED LUMI ANIMATIONS")]
    public void GenerateEnhancedLumiAnimations()
    {
        Debug.Log("✨ Generating enhanced cute fox animations...");
        
        GenerateLumiIdle();
        GenerateLumiHappy();
        GenerateLumiWalk();
        GenerateLumiJump();
        GenerateLumiCelebrate();
        GenerateLumiThinking();
        GenerateLumiEncourage();
        GenerateLumiSad();
        GenerateLumiSleeping();
        GenerateLumiTalking();
        
        Debug.Log("🎉 Enhanced cute fox animations complete!");
        Debug.Log("📁 Files saved to: Assets/Art/Characters/Lumi/");
    }
    
    #region Enhanced Animation Generators
    
    [ContextMenu("Generate Enhanced Idle")]
    public void GenerateLumiIdle()
    {
        for (int frame = 0; frame < idleFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / idleFrames;
            
            // Gentle breathing motion
            float breathe = Mathf.Sin(t * Mathf.PI * 2) * 3f;
            float earWiggle = Mathf.Sin(t * Mathf.PI * 3) * 2f;
            float tailSway = Mathf.Sin(t * Mathf.PI * 2) * 4f;
            
            DrawCuteFox(texture, Vector2.zero, breathe, earWiggle, 0f, tailSway, 1f, false, false, 0f);
            SaveTexture(texture, $"Lumi_Idle_{frame:D2}");
        }
        Debug.Log("💤 Enhanced idle: Gentle breathing with ear wiggles");
    }
    
    [ContextMenu("Generate Enhanced Happy")]
    public void GenerateLumiHappy()
    {
        for (int frame = 0; frame < happyFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / happyFrames;
            
            // Excited bouncing
            float bounce = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 4)) * 25f;
            float tailWag = Mathf.Sin(t * Mathf.PI * 8) * 20f;
            float eyeScale = 1.2f; // Bigger eyes when happy
            
            DrawCuteFox(texture, new Vector2(0, bounce), 0f, 5f, 0f, tailWag, eyeScale, true, false, 0f);
            DrawSimpleSparkles(texture, frame);
            
            SaveTexture(texture, $"Lumi_Happy_{frame:D2}");
        }
        Debug.Log("🎉 Enhanced happy: Bouncing with big eyes!");
    }
    
    [ContextMenu("Generate Enhanced Walk")]
    public void GenerateLumiWalk()
    {
        for (int frame = 0; frame < walkFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / walkFrames;
            
            // Walking motion
            float bob = Mathf.Sin(t * Mathf.PI * 2) * 6f;
            float legMove = Mathf.Cos(t * Mathf.PI * 2) * 12f;
            float tailSway = Mathf.Sin(t * Mathf.PI * 2) * 8f;
            
            DrawCuteFox(texture, new Vector2(0, bob), bob * 0.5f, 0f, legMove, tailSway, 1f, false, false, 0f);
            SaveTexture(texture, $"Lumi_Walk_{frame:D2}");
        }
        Debug.Log("🚶 Enhanced walk: Cute trotting motion");
    }
    
    [ContextMenu("Generate Enhanced Jump")]
    public void GenerateLumiJump()
    {
        for (int frame = 0; frame < jumpFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / (jumpFrames - 1);
            
            // Jump arc
            float jumpHeight = Mathf.Sin(t * Mathf.PI) * 60f;
            float earFlap = -Mathf.Sin(t * Mathf.PI) * 10f;
            float tailUp = Mathf.Sin(t * Mathf.PI) * 25f;
            float eyeScale = 1.1f; // Excited eyes
            
            DrawCuteFox(texture, new Vector2(0, jumpHeight), 0f, earFlap, 0f, tailUp, eyeScale, false, false, 0f);
            
            // Motion lines during peak
            if (t > 0.3f && t < 0.7f) DrawSimpleMotionLines(texture);
            
            SaveTexture(texture, $"Lumi_Jump_{frame:D2}");
        }
        Debug.Log("🦘 Enhanced jump: Smooth jumping arc");
    }
    
    [ContextMenu("Generate Enhanced Celebrate")]
    public void GenerateLumiCelebrate()
    {
        for (int frame = 0; frame < celebrateFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / celebrateFrames;
            
            // Celebration motion
            float bounce = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 5)) * 20f;
            float spin = t * 360f;
            float eyeScale = 1.3f; // Maximum excitement
            
            DrawCuteFox(texture, new Vector2(0, bounce), 0f, 5f, 0f, 0f, eyeScale, true, false, 0f);
            DrawCelebrationStars(texture, frame);
            
            SaveTexture(texture, $"Lumi_Celebrate_{frame:D2}");
        }
        Debug.Log("🌟 Enhanced celebrate: Victory celebration!");
    }
    
    [ContextMenu("Generate Enhanced Thinking")]
    public void GenerateLumiThinking()
    {
        for (int frame = 0; frame < thinkingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / thinkingFrames;
            
            // Thinking pose
            float headTilt = Mathf.Sin(t * Mathf.PI) * 10f;
            float earCurious = Mathf.Sin(t * Mathf.PI * 2) * 3f;
            float eyeScale = 0.9f; // Squinted thinking eyes
            
            DrawCuteFox(texture, Vector2.zero, 0f, earCurious, 0f, 0f, eyeScale, false, false, 0f);
            DrawThoughtBubble(texture, frame);
            
            SaveTexture(texture, $"Lumi_Thinking_{frame:D2}");
        }
        Debug.Log("🤔 Enhanced thinking: Curious pondering");
    }
    
    [ContextMenu("Generate Enhanced Encourage")]
    public void GenerateLumiEncourage()
    {
        for (int frame = 0; frame < encourageFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / encourageFrames;
            
            // Encouraging nod
            float nod = Mathf.Sin(t * Mathf.PI * 3) * 8f;
            float eyeScale = 1.15f; // Warm, encouraging eyes
            
            DrawCuteFox(texture, new Vector2(0, nod), nod * 0.5f, 0f, 0f, 0f, eyeScale, true, false, 0f);
            DrawEncouragementHearts(texture, frame);
            
            SaveTexture(texture, $"Lumi_Encourage_{frame:D2}");
        }
        Debug.Log("💚 Enhanced encourage: Supportive nodding");
    }
    
    [ContextMenu("Generate Enhanced Sad")]
    public void GenerateLumiSad()
    {
        for (int frame = 0; frame < sadFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / sadFrames;
            
            // Sad expression
            float slowBreathe = Mathf.Sin(t * Mathf.PI) * 2f;
            float earDroop = -12f;
            float tailDroop = -8f;
            float eyeScale = 0.8f; // Smaller, sad eyes
            
            DrawCuteFox(texture, Vector2.zero, slowBreathe, earDroop, 0f, tailDroop, eyeScale, false, true, 0f);
            
            SaveTexture(texture, $"Lumi_Sad_{frame:D2}");
        }
        Debug.Log("😢 Enhanced sad: Gentle empathy");
    }
    
    [ContextMenu("Generate Enhanced Sleeping")]
    public void GenerateLumiSleeping()
    {
        for (int frame = 0; frame < sleepingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / sleepingFrames;
            
            // Sleeping breathing
            float breathe = Mathf.Sin(t * Mathf.PI * 2) * 2f;
            
            DrawSleepingCuteFox(texture, breathe);
            DrawSleepingZZZ(texture, frame);
            
            SaveTexture(texture, $"Lumi_Sleeping_{frame:D2}");
        }
        Debug.Log("😴 Enhanced sleeping: Peaceful rest");
    }
    
    [ContextMenu("Generate Enhanced Talking")]
    public void GenerateLumiTalking()
    {
        for (int frame = 0; frame < talkingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / talkingFrames;
            
            // Talking mouth movement
            float mouthOpen = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 3)) * 0.8f;
            float earMovement = Mathf.Sin(t * Mathf.PI * 6) * 2f;
            
            DrawCuteFox(texture, Vector2.zero, 0f, earMovement, 0f, 0f, 1f, false, false, mouthOpen);
            
            SaveTexture(texture, $"Lumi_Talking_{frame:D2}");
        }
        Debug.Log("💬 Enhanced talking: Expressive speech");
    }
    
    #endregion
    
    #region Enhanced Drawing Methods - Simple Cute Style
    
    void DrawCuteFox(Texture2D tex, Vector2 offset, float breathe, float earAngle, 
                    float legOffset, float tailAngle, float eyeScale, 
                    bool showExcited = false, bool sadExpression = false, float mouthOpen = 0f)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2) + offset;
        
        // Drawing order: back to front
        DrawCuteTail(tex, center + new Vector2(-40, -50 + breathe), tailAngle);
        DrawCuteBody(tex, center + new Vector2(0, -30 + breathe));
        DrawCuteLegs(tex, center + new Vector2(0, -80 + breathe), legOffset);
        DrawCuteHead(tex, center + new Vector2(0, 60 + breathe));
        DrawCuteEars(tex, center + new Vector2(0, 120 + breathe), earAngle);
        DrawCuteFace(tex, center + new Vector2(0, 60 + breathe), eyeScale, showExcited, sadExpression, mouthOpen);
    }
    
    void DrawCuteHead(Texture2D tex, Vector2 center)
    {
        // Large round head (main feature for cuteness)
        DrawCircle(tex, center, (int)(headSize / 2), bodyColor);
        DrawCircleOutline(tex, center, (int)(headSize / 2), outlineColor, (int)outlineWidth);
        
        // White face area (simplified)
        DrawCircle(tex, center + new Vector2(0, -15), (int)(headSize * 0.4f), whiteColor);
    }
    
    void DrawCuteBody(Texture2D tex, Vector2 center)
    {
        // Simple oval body
        DrawEllipse(tex, center, (int)(bodySize * 0.6f), (int)(bodySize * 0.5f), bodyColor);
        DrawEllipseOutline(tex, center, (int)(bodySize * 0.6f), (int)(bodySize * 0.5f), outlineColor, (int)outlineWidth);
        
        // White chest
        DrawEllipse(tex, center + new Vector2(0, 10), (int)(bodySize * 0.4f), (int)(bodySize * 0.3f), whiteColor);
    }
    
    void DrawCuteTail(Texture2D tex, Vector2 basePos, float angle)
    {
        Vector2 tailPos = basePos + new Vector2(angle * 0.3f, 0);
        
        // Fluffy tail
        DrawEllipse(tex, tailPos, (int)(bodySize * 0.4f), (int)(bodySize * 0.25f), bodyColor, -30f + angle);
        DrawEllipseOutline(tex, tailPos, (int)(bodySize * 0.4f), (int)(bodySize * 0.25f), outlineColor, (int)outlineWidth, -30f + angle);
        
        // White tip
        Vector2 tipOffset = RotatePoint(new Vector2(-35, -10), Vector2.zero, -30f + angle);
        DrawCircle(tex, tailPos + tipOffset, (int)(bodySize * 0.15f), whiteColor);
        DrawCircleOutline(tex, tailPos + tipOffset, (int)(bodySize * 0.15f), outlineColor, (int)outlineWidth);
    }
    
    void DrawCuteEars(Texture2D tex, Vector2 basePos, float angle)
    {
        // Left ear
        Vector2 leftEarTip = basePos + new Vector2(-45, 30 + angle);
        Vector2 leftEarBase1 = basePos + new Vector2(-60, 0);
        Vector2 leftEarBase2 = basePos + new Vector2(-30, 0);
        
        FillTriangle(tex, leftEarTip, leftEarBase1, leftEarBase2, bodyColor);
        DrawTriangleOutline(tex, leftEarTip, leftEarBase1, leftEarBase2, outlineColor, (int)outlineWidth);
        
        // Left inner ear
        Vector2 leftInnerTip = basePos + new Vector2(-45, 20 + angle);
        Vector2 leftInnerBase1 = basePos + new Vector2(-50, 5);
        Vector2 leftInnerBase2 = basePos + new Vector2(-40, 5);
        FillTriangle(tex, leftInnerTip, leftInnerBase1, leftInnerBase2, darkOrangeColor);
        
        // Right ear
        Vector2 rightEarTip = basePos + new Vector2(45, 30 - angle * 0.5f);
        Vector2 rightEarBase1 = basePos + new Vector2(30, 0);
        Vector2 rightEarBase2 = basePos + new Vector2(60, 0);
        
        FillTriangle(tex, rightEarTip, rightEarBase1, rightEarBase2, bodyColor);
        DrawTriangleOutline(tex, rightEarTip, rightEarBase1, rightEarBase2, outlineColor, (int)outlineWidth);
        
        // Right inner ear
        Vector2 rightInnerTip = basePos + new Vector2(45, 20 - angle * 0.5f);
        Vector2 rightInnerBase1 = basePos + new Vector2(40, 5);
        Vector2 rightInnerBase2 = basePos + new Vector2(50, 5);
        FillTriangle(tex, rightInnerTip, rightInnerBase1, rightInnerBase2, darkOrangeColor);
    }
    
    void DrawCuteLegs(Texture2D tex, Vector2 center, float offset)
    {
        // Back legs
        DrawEllipse(tex, center + new Vector2(-25, offset), 18, 25, bodyColor);
        DrawEllipse(tex, center + new Vector2(25, -offset), 18, 25, bodyColor);
        
        // Front legs
        DrawEllipse(tex, center + new Vector2(-40, offset * 0.5f), 15, 20, bodyColor);
        DrawEllipse(tex, center + new Vector2(40, -offset * 0.5f), 15, 20, bodyColor);
        
        // Add outlines
        DrawEllipseOutline(tex, center + new Vector2(-25, offset), 18, 25, outlineColor, (int)outlineWidth);
        DrawEllipseOutline(tex, center + new Vector2(25, -offset), 18, 25, outlineColor, (int)outlineWidth);
        DrawEllipseOutline(tex, center + new Vector2(-40, offset * 0.5f), 15, 20, outlineColor, (int)outlineWidth);
        DrawEllipseOutline(tex, center + new Vector2(40, -offset * 0.5f), 15, 20, outlineColor, (int)outlineWidth);
    }
    
    void DrawCuteFace(Texture2D tex, Vector2 center, float eyeScale, bool showExcited, bool sadExpression, float mouthOpen)
    {
        if (sadExpression)
        {
            DrawSadEyes(tex, center, eyeScale);
            DrawSadMouth(tex, center);
        }
        else if (mouthOpen > 0)
        {
            DrawOpenEyes(tex, center, eyeScale);
            DrawTalkingMouth(tex, center, mouthOpen);
        }
        else if (showExcited)
        {
            DrawExcitedEyes(tex, center, eyeScale);
            DrawHappyMouth(tex, center, true);
        }
        else
        {
            DrawNormalEyes(tex, center, eyeScale);
            DrawHappyMouth(tex, center, false);
        }
        
        // Nose (always same)
        DrawTriangleNose(tex, center + new Vector2(0, -5));
        
        // Blush marks
        DrawCircle(tex, center + new Vector2(-35, -10), 8, blushColor);
        DrawCircle(tex, center + new Vector2(35, -10), 8, blushColor);
    }
    
    void DrawNormalEyes(Texture2D tex, Vector2 center, float scale)
    {
        int eyeRadius = (int)(eyeSize * scale);
        
        // Left eye
        DrawCircle(tex, center + new Vector2(-25, 0), eyeRadius, Color.white);
        DrawCircle(tex, center + new Vector2(-25, 0), eyeRadius - 2, eyeColor);
        DrawCircleOutline(tex, center + new Vector2(-25, 0), eyeRadius, outlineColor, 2);
        
        // Right eye
        DrawCircle(tex, center + new Vector2(25, 0), eyeRadius, Color.white);
        DrawCircle(tex, center + new Vector2(25, 0), eyeRadius - 2, eyeColor);
        DrawCircleOutline(tex, center + new Vector2(25, 0), eyeRadius, outlineColor, 2);
    }
    
    void DrawExcitedEyes(Texture2D tex, Vector2 center, float scale)
    {
        int eyeRadius = (int)(eyeSize * scale);
        
        // Larger, more open eyes
        DrawCircle(tex, center + new Vector2(-25, 0), eyeRadius, Color.white);
        DrawCircle(tex, center + new Vector2(-25, -2), (int)(eyeRadius * 0.7f), eyeColor);
        DrawCircleOutline(tex, center + new Vector2(-25, 0), eyeRadius, outlineColor, 2);
        
        DrawCircle(tex, center + new Vector2(25, 0), eyeRadius, Color.white);
        DrawCircle(tex, center + new Vector2(25, -2), (int)(eyeRadius * 0.7f), eyeColor);
        DrawCircleOutline(tex, center + new Vector2(25, 0), eyeRadius, outlineColor, 2);
        
        // Sparkle dots
        DrawCircle(tex, center + new Vector2(-20, 5), 3, Color.white);
        DrawCircle(tex, center + new Vector2(30, 5), 3, Color.white);
    }
    
    void DrawSadEyes(Texture2D tex, Vector2 center, float scale)
    {
        // Downturned eyes
        int eyeWidth = (int)(eyeSize * 1.5f * scale);
        
        // Left sad eye
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float curve = (float)(i * i) / (eyeWidth/2) * 0.5f;
            Vector2 pos = center + new Vector2(-25 + i, curve - 2);
            DrawCircle(tex, pos, 2, eyeColor);
        }
        
        // Right sad eye
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float curve = (float)(i * i) / (eyeWidth/2) * 0.5f;
            Vector2 pos = center + new Vector2(25 + i, curve - 2);
            DrawCircle(tex, pos, 2, eyeColor);
        }
    }
    
    void DrawOpenEyes(Texture2D tex, Vector2 center, float scale)
    {
        DrawNormalEyes(tex, center, scale);
    }
    
    void DrawHappyMouth(Texture2D tex, Vector2 center, bool showTongue)
    {
        // Simple smile
        int mouthWidth = 30;
        
        for (int i = -mouthWidth; i <= mouthWidth; i++)
        {
            float curve = -(float)(i * i) / (mouthWidth * 0.8f);
            Vector2 pos = center + new Vector2(i, curve - 20);
            DrawCircle(tex, pos, 2, outlineColor);
        }
        
        if (showTongue)
        {
            DrawEllipse(tex, center + new Vector2(0, -25), 10, 8, tongueColor);
        }
    }
    
    void DrawSadMouth(Texture2D tex, Vector2 center)
    {
        // Downturned mouth
        int mouthWidth = 25;
        
        for (int i = -mouthWidth; i <= mouthWidth; i++)
        {
            float curve = (float)(i * i) / (mouthWidth * 0.8f) * 0.3f;
            Vector2 pos = center + new Vector2(i, curve - 20);
            DrawCircle(tex, pos, 2, outlineColor);
        }
    }
    
    void DrawTalkingMouth(Texture2D tex, Vector2 center, float openAmount)
    {
        // Oval mouth that changes size
        int width = (int)(10 + openAmount * 15);
        int height = (int)(5 + openAmount * 10);
        
        DrawEllipse(tex, center + new Vector2(0, -20), width, height, tongueColor);
        DrawEllipseOutline(tex, center + new Vector2(0, -20), width, height, outlineColor, 2);
    }
    
    void DrawTriangleNose(Texture2D tex, Vector2 pos)
    {
        Vector2 top = pos + new Vector2(0, 2);
        Vector2 left = pos + new Vector2(-4, -4);
        Vector2 right = pos + new Vector2(4, -4);
        
        FillTriangle(tex, top, left, right, outlineColor);
    }
    
    void DrawSleepingCuteFox(Texture2D tex, float breathe)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2 - 10);
        
        // Curled sleeping position
        DrawEllipse(tex, center, (int)(bodySize * 0.8f), (int)(bodySize * 0.6f), bodyColor, 15f);
        DrawEllipseOutline(tex, center, (int)(bodySize * 0.8f), (int)(bodySize * 0.6f), outlineColor, (int)outlineWidth, 15f);
        
        // Head tucked in
        DrawCircle(tex, center + new Vector2(40, 20), (int)(headSize * 0.6f), bodyColor);
        DrawCircleOutline(tex, center + new Vector2(40, 20), (int)(headSize * 0.6f), outlineColor, (int)outlineWidth);
        
        // Closed eyes
        for (int i = -12; i <= 12; i++)
        {
            Vector2 leftPos = center + new Vector2(25 + i, 25 - i * i * 0.02f);
            Vector2 rightPos = center + new Vector2(55 + i, 25 - i * i * 0.02f);
            DrawCircle(tex, leftPos, 2, outlineColor);
            DrawCircle(tex, rightPos, 2, outlineColor);
        }
        
        // Small nose
        DrawTriangleNose(tex, center + new Vector2(40, 15));
    }
    
    #endregion
    
    #region Enhanced Effect Methods
    
    void DrawSimpleSparkles(Texture2D tex, int frame)
    {
        System.Random rand = new System.Random(frame);
        for (int i = 0; i < 5; i++)
        {
            Vector2 pos = new Vector2(rand.Next(100, spriteSize - 100), rand.Next(100, spriteSize - 100));
            DrawStar(tex, pos, 4 + rand.Next(3), frame * 30f + i * 45f);
        }
    }
    
    void DrawCelebrationStars(Texture2D tex, int frame)
    {
        System.Random rand = new System.Random(frame * 3);
        
        for (int i = 0; i < 8; i++)
        {
            Vector2 pos = new Vector2(rand.Next(50, spriteSize - 50), rand.Next(50, spriteSize - 50));
            DrawStar(tex, pos, 5 + rand.Next(4), frame * 45f + i * 30f);
        }
    }
    
    void DrawSimpleMotionLines(Texture2D tex)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2);
        for (int i = 0; i < 3; i++)
        {
            Vector2 start = center + new Vector2(-60 - i * 12, 20 - i * 6);
            Vector2 end = start + new Vector2(-20, 2);
            DrawThickLine(tex, start, end, 2, new Color(0.5f, 0.5f, 0.5f, 0.7f));
        }
    }
    
    void DrawThoughtBubble(Texture2D tex, int frame)
    {
        if (frame % 4 == 0)
        {
            Vector2 pos = new Vector2(spriteSize / 2 + 80, spriteSize / 2 + 120);
            
            // Main bubble
            DrawCircle(tex, pos, 20, Color.white);
            DrawCircleOutline(tex, pos, 20, outlineColor, 2);
            
            // Question mark
            DrawThickLine(tex, pos + new Vector2(0, -5), pos + new Vector2(0, 8), 3, outlineColor);
            DrawCircle(tex, pos + new Vector2(0, -12), 3, outlineColor);
        }
    }
    
    void DrawEncouragementHearts(Texture2D tex, int frame)
    {
        if (frame % 2 == 0)
        {
            Vector2 pos = new Vector2(spriteSize / 2 - 60, spriteSize / 2 + 100);
            DrawSimpleHeart(tex, pos, 12);
        }
    }
    
    void DrawSimpleHeart(Texture2D tex, Vector2 center, int size)
    {
        // Simple heart shape
        DrawCircle(tex, center + new Vector2(-size/3, size/3), size/2, new Color(1f, 0.3f, 0.4f));
        DrawCircle(tex, center + new Vector2(size/3, size/3), size/2, new Color(1f, 0.3f, 0.4f));
        
        Vector2 bottom = center + new Vector2(0, -size/2);
        FillTriangle(tex, center + new Vector2(-size/2, size/4), center + new Vector2(size/2, size/4), bottom, new Color(1f, 0.3f, 0.4f));
    }
    
    void DrawSleepingZZZ(Texture2D tex, int frame)
    {
        float offset = frame * 8f;
        Vector2 basePos = new Vector2(spriteSize / 2 + 80, spriteSize / 2 + 100 - offset);
        
        if (offset < 60)
        {
            DrawZLetter(tex, basePos, 12);
            if (offset > 20) DrawZLetter(tex, basePos + new Vector2(15, 20), 10);
            if (offset > 40) DrawZLetter(tex, basePos + new Vector2(30, 40), 8);
        }
    }
    
    void DrawZLetter(Texture2D tex, Vector2 pos, int size)
    {
        Color zColor = new Color(0.6f, 0.7f, 1f, 0.8f);
        
        DrawThickLine(tex, pos, pos + new Vector2(size, 0), 3, zColor);
        DrawThickLine(tex, pos + new Vector2(size, 0), pos + new Vector2(0, size), 3, zColor);
        DrawThickLine(tex, pos + new Vector2(0, size), pos + new Vector2(size, size), 3, zColor);
    }
    
    void DrawStar(Texture2D tex, Vector2 center, int size, float rotation)
    {
        Vector2[] points = new Vector2[10];
        for (int i = 0; i < 10; i++)
        {
            float angle = (i * 36f - 90f + rotation) * Mathf.Deg2Rad;
            float radius = (i % 2 == 0) ? size : size * 0.4f;
            points[i] = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }
        
        Color starColor = new Color(1f, 0.9f, 0.3f); // Yellow star
        
        for (int i = 0; i < 10; i++)
        {
            FillTriangle(tex, center, points[i], points[(i + 1) % 10], starColor);
        }
    }
    
    #endregion
    
    #region Utility Methods (Same as before)
    
    Texture2D CreateTexture()
    {
        Texture2D tex = new Texture2D(spriteSize, spriteSize, TextureFormat.ARGB32, false);
        tex.filterMode = FilterMode.Point;
        Color[] clearColors = new Color[spriteSize * spriteSize];
        for (int i = 0; i < clearColors.Length; i++)
            clearColors[i] = Color.clear;
        tex.SetPixels(clearColors);
        return tex;
    }
    
    void DrawCircle(Texture2D tex, Vector2 center, int radius, Color color)
    {
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int px = (int)center.x + x;
                    int py = (int)center.y + y;
                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        tex.SetPixel(px, py, color);
                    }
                }
            }
        }
    }
    
    void DrawCircleOutline(Texture2D tex, Vector2 center, int radius, Color color, int thickness)
    {
        for (int y = -radius - thickness; y <= radius + thickness; y++)
        {
            for (int x = -radius - thickness; x <= radius + thickness; x++)
            {
                float dist = Mathf.Sqrt(x * x + y * y);
                if (dist >= radius - thickness/2 && dist <= radius + thickness)
                {
                    int px = (int)center.x + x;
                    int py = (int)center.y + y;
                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        tex.SetPixel(px, py, color);
                    }
                }
            }
        }
    }
    
    void DrawEllipse(Texture2D tex, Vector2 center, int radiusX, int radiusY, Color color, float rotation = 0f, float alpha = 1f)
    {
        float rotRad = rotation * Mathf.Deg2Rad;
        int maxRadius = Mathf.Max(radiusX, radiusY) + 5;
        
        for (int y = -maxRadius; y <= maxRadius; y++)
        {
            for (int x = -maxRadius; x <= maxRadius; x++)
            {
                float rotX = x * Mathf.Cos(rotRad) - y * Mathf.Sin(rotRad);
                float rotY = x * Mathf.Sin(rotRad) + y * Mathf.Cos(rotRad);
                
                float dist = (rotX * rotX) / (radiusX * radiusX) + (rotY * rotY) / (radiusY * radiusY);
                if (dist <= 1f)
                {
                    int px = (int)center.x + x;
                    int py = (int)center.y + y;
                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        Color finalColor = color;
                        finalColor.a *= alpha;
                        tex.SetPixel(px, py, finalColor);
                    }
                }
            }
        }
    }
    
    void DrawEllipseOutline(Texture2D tex, Vector2 center, int radiusX, int radiusY, Color color, int thickness, float rotation = 0f)
    {
        float rotRad = rotation * Mathf.Deg2Rad;
        int maxRadius = Mathf.Max(radiusX, radiusY) + thickness + 5;
        
        for (int y = -maxRadius; y <= maxRadius; y++)
        {
            for (int x = -maxRadius; x <= maxRadius; x++)
            {
                float rotX = x * Mathf.Cos(rotRad) - y * Mathf.Sin(rotRad);
                float rotY = x * Mathf.Sin(rotRad) + y * Mathf.Cos(rotRad);
                
                float dist = (rotX * rotX) / (radiusX * radiusX) + (rotY * rotY) / (radiusY * radiusY);
                float outerDist = (rotX * rotX) / ((radiusX + thickness) * (radiusX + thickness)) + 
                                  (rotY * rotY) / ((radiusY + thickness) * (radiusY + thickness));
                
                if (outerDist <= 1f && dist >= 1f)
                {
                    int px = (int)center.x + x;
                    int py = (int)center.y + y;
                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        tex.SetPixel(px, py, color);
                    }
                }
            }
        }
    }
    
    void FillTriangle(Texture2D tex, Vector2 p1, Vector2 p2, Vector2 p3, Color color)
    {
        float minX = Mathf.Min(p1.x, Mathf.Min(p2.x, p3.x));
        float maxX = Mathf.Max(p1.x, Mathf.Max(p2.x, p3.x));
        float minY = Mathf.Min(p1.y, Mathf.Min(p2.y, p3.y));
        float maxY = Mathf.Max(p1.y, Mathf.Max(p2.y, p3.y));
        
        for (int y = (int)minY; y <= (int)maxY; y++)
        {
            for (int x = (int)minX; x <= (int)maxX; x++)
            {
                if (x >= 0 && x < tex.width && y >= 0 && y < tex.height)
                {
                    Vector2 p = new Vector2(x, y);
                    if (IsPointInTriangle(p, p1, p2, p3))
                    {
                        tex.SetPixel(x, y, color);
                    }
                }
            }
        }
    }
    
    void DrawTriangleOutline(Texture2D tex, Vector2 p1, Vector2 p2, Vector2 p3, Color color, int thickness)
    {
        DrawThickLine(tex, p1, p2, thickness, color);
        DrawThickLine(tex, p2, p3, thickness, color);
        DrawThickLine(tex, p3, p1, thickness, color);
    }
    
    bool IsPointInTriangle(Vector2 p, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float d1 = Sign(p, p1, p2);
        float d2 = Sign(p, p2, p3);
        float d3 = Sign(p, p3, p1);
        
        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);
        
        return !(hasNeg && hasPos);
    }
    
    float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }
    
    void DrawThickLine(Texture2D tex, Vector2 p1, Vector2 p2, int thickness, Color color)
    {
        int steps = (int)Vector2.Distance(p1, p2) + 1;
        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector2 pixel = Vector2.Lerp(p1, p2, t);
            DrawCircle(tex, pixel, thickness, color);
        }
    }
    
    Vector2 RotatePoint(Vector2 point, Vector2 pivot, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = point - pivot;
        return pivot + new Vector2(
            dir.x * Mathf.Cos(rad) - dir.y * Mathf.Sin(rad),
            dir.x * Mathf.Sin(rad) + dir.y * Mathf.Cos(rad)
        );
    }
    
    void SaveTexture(Texture2D tex, string filename)
    {
        tex.Apply();
        
        string directory = Path.Combine(Application.dataPath, "Art", "Characters", "Lumi");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        byte[] bytes = tex.EncodeToPNG();
        string filePath = Path.Combine(directory, filename + ".png");
        File.WriteAllBytes(filePath, bytes);
        
        Debug.Log($"✅ Saved: {filename}.png");
    }
    
    #endregion
}