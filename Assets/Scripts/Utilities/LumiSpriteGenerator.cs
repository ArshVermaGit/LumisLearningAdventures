// Assets/Scripts/Utilities/LumiSpriteGenerator.cs
using UnityEngine;
using System.Collections;
using System.IO;

public class LumiSpriteGenerator : MonoBehaviour
{
    [Header("Lumi Character Generator - Cute Cartoon Fox")]
    public int spriteSize = 512;
    
    [Header("Cartoon Fox Colors - Based on Reference")]
    public Color bodyColor = new Color(1f, 0.6f, 0.2f); // Bright Orange #FF9933
    public Color darkOrangeColor = new Color(0.8f, 0.35f, 0.15f); // Dark Orange/Brown #CC5926
    public Color accentColor = new Color(1f, 0.98f, 0.94f); // Cream White #FFFEF0
    public Color outlineColor = new Color(0.1f, 0.1f, 0.1f); // Black outline #1A1A1A
    public Color eyeColor = new Color(0.05f, 0.05f, 0.05f); // Very Dark #0D0D0D
    public Color tongueColor = new Color(1f, 0.4f, 0.6f); // Pink Tongue #FF6699
    public Color blushColor = new Color(1f, 0.75f, 0.8f); // Light Pink Blush #FFBFCC
    public Color noseColor = new Color(0.1f, 0.1f, 0.1f); // Black Nose
    
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
    
    [Header("Character Proportions")]
    public float outlineThickness = 3f;
    public float headScale = 1.2f; // Large head proportion
    public float bodyScale = 0.7f; // Smaller body
    
    [ContextMenu("🎨 GENERATE ALL LUMI ANIMATIONS")]
    public void GenerateAllLumiAnimations()
    {
        Debug.Log("✨ Starting Lumi's magical transformation (Cartoon Style)...");
        
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
        
        Debug.Log("🎉 All Lumi animations generated! Cartoon fox ready!");
        Debug.Log("📁 Files saved to: Assets/Art/Characters/Lumi/");
    }
    
    #region Individual Animation Generators
    
    [ContextMenu("Generate Idle Animation")]
    public void GenerateLumiIdle()
    {
        for (int frame = 0; frame < idleFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / idleFrames;
            
            // Gentle breathing and ear wiggle
            float breathe = Mathf.Sin(t * Mathf.PI * 2) * 3f;
            float earWiggle = Mathf.Sin(t * Mathf.PI * 3) * 1.5f;
            float tailSway = Mathf.Sin(t * Mathf.PI * 2) * 2f;
            float blink = (frame == idleFrames / 2) ? 0.3f : 1f; // Blink halfway
            
            DrawCartoonFox(texture, Vector2.zero, breathe, earWiggle, 0f, tailSway, blink, false, false, 0f);
            SaveTexture(texture, $"Lumi_Idle_{frame:D2}");
        }
        Debug.Log("💤 Idle animation: Breathing peacefully");
    }
    
    [ContextMenu("Generate Happy Animation")]
    public void GenerateLumiHappy()
    {
        for (int frame = 0; frame < happyFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / happyFrames;
            
            // Excited bouncing
            float bounce = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 4)) * 20f;
            float tailWag = Mathf.Sin(t * Mathf.PI * 8) * 15f;
            float earPerk = Mathf.Sin(t * Mathf.PI * 4) * 3f;
            
            DrawCartoonFox(texture, new Vector2(0, bounce), 0f, earPerk, 0f, tailWag, 1f, true, false, 0f);
            
            // Add sparkles
            DrawHappySparkles(texture, frame);
            
            SaveTexture(texture, $"Lumi_Happy_{frame:D2}");
        }
        Debug.Log("🎉 Happy animation: Bouncing with joy!");
    }
    
    [ContextMenu("Generate Walk Animation")]
    public void GenerateLumiWalk()
    {
        for (int frame = 0; frame < walkFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / walkFrames;
            
            // Walking bob and movement
            float bob = Mathf.Sin(t * Mathf.PI * 2) * 5f;
            float legMove = Mathf.Cos(t * Mathf.PI * 2) * 10f;
            float tailSway = Mathf.Sin(t * Mathf.PI * 2 + Mathf.PI/4) * 12f;
            
            DrawCartoonFox(texture, new Vector2(0, bob), bob * 0.5f, 0f, legMove, tailSway, 1f, false, false, 0f);
            SaveTexture(texture, $"Lumi_Walk_{frame:D2}");
        }
        Debug.Log("🚶 Walk animation: Cute trotting");
    }
    
    [ContextMenu("Generate Jump Animation")]
    public void GenerateLumiJump()
    {
        for (int frame = 0; frame < jumpFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / (jumpFrames - 1);
            
            // Jump arc
            float jumpHeight = Mathf.Sin(t * Mathf.PI) * 50f;
            float earFlap = -Mathf.Sin(t * Mathf.PI) * 8f;
            float tailUp = Mathf.Sin(t * Mathf.PI) * 20f;
            
            DrawCartoonFox(texture, new Vector2(0, jumpHeight), 0f, earFlap, 0f, tailUp, 1.1f, false, false, 0f);
            
            // Motion lines during peak
            if (t > 0.3f && t < 0.7f) DrawMotionLines(texture);
            
            SaveTexture(texture, $"Lumi_Jump_{frame:D2}");
        }
        Debug.Log("🦘 Jump animation: Leaping!");
    }
    
    [ContextMenu("Generate Celebrate Animation")]
    public void GenerateLumiCelebrate()
    {
        for (int frame = 0; frame < celebrateFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / celebrateFrames;
            
            // Spinning celebration
            float bounce = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 5)) * 15f;
            float rotate = Mathf.Sin(t * Mathf.PI * 2) * 10f;
            
            DrawCartoonFox(texture, new Vector2(0, bounce), 0f, 5f, 0f, 0f, 1f, true, false, 0f);
            DrawCelebrationEffects(texture, frame);
            
            SaveTexture(texture, $"Lumi_Celebrate_{frame:D2}");
        }
        Debug.Log("🌟 Celebrate animation: Victory dance!");
    }
    
    [ContextMenu("Generate Thinking Animation")]
    public void GenerateLumiThinking()
    {
        for (int frame = 0; frame < thinkingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / thinkingFrames;
            
            // Head tilt
            float headTilt = Mathf.Sin(t * Mathf.PI) * 8f;
            float earCurious = Mathf.Sin(t * Mathf.PI * 2) * 3f;
            
            DrawCartoonFox(texture, Vector2.zero, 0f, earCurious, 0f, 0f, 0.8f, false, false, 0f);
            DrawThoughtBubble(texture, frame);
            
            SaveTexture(texture, $"Lumi_Thinking_{frame:D2}");
        }
        Debug.Log("🤔 Thinking animation: Curious pondering");
    }
    
    [ContextMenu("Generate Encourage Animation")]
    public void GenerateLumiEncourage()
    {
        for (int frame = 0; frame < encourageFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / encourageFrames;
            
            // Nodding
            float nod = Mathf.Sin(t * Mathf.PI * 3) * 6f;
            
            DrawCartoonFox(texture, new Vector2(0, nod), nod * 0.5f, 0f, 0f, 0f, 1f, true, false, 0f);
            DrawEncouragementHearts(texture, frame);
            
            SaveTexture(texture, $"Lumi_Encourage_{frame:D2}");
        }
        Debug.Log("💚 Encourage animation: Supportive nodding");
    }
    
    [ContextMenu("Generate Sad Animation")]
    public void GenerateLumiSad()
    {
        for (int frame = 0; frame < sadFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / sadFrames;
            
            // Droopy and slow
            float slowBreathe = Mathf.Sin(t * Mathf.PI) * 2f;
            
            DrawCartoonFox(texture, Vector2.zero, slowBreathe, -8f, 0f, -10f, 1f, false, true, 0f);
            
            SaveTexture(texture, $"Lumi_Sad_{frame:D2}");
        }
        Debug.Log("😢 Sad animation: Gentle empathy");
    }
    
    [ContextMenu("Generate Sleeping Animation")]
    public void GenerateLumiSleeping()
    {
        for (int frame = 0; frame < sleepingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / sleepingFrames;
            
            // Peaceful breathing
            float breathe = Mathf.Sin(t * Mathf.PI * 2) * 2f;
            
            DrawSleepingCartoonFox(texture, breathe);
            DrawSleepingZ(texture, frame);
            
            SaveTexture(texture, $"Lumi_Sleeping_{frame:D2}");
        }
        Debug.Log("😴 Sleeping animation: Peaceful rest");
    }
    
    [ContextMenu("Generate Talking Animation")]
    public void GenerateLumiTalking()
    {
        for (int frame = 0; frame < talkingFrames; frame++)
        {
            Texture2D texture = CreateTexture();
            float t = (float)frame / talkingFrames;
            
            // Mouth movement
            float mouthOpen = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 3)) * 0.7f;
            float earMovement = Mathf.Sin(t * Mathf.PI * 6) * 2f;
            
            DrawCartoonFox(texture, Vector2.zero, 0f, earMovement, 0f, 0f, 1f, false, false, mouthOpen);
            
            SaveTexture(texture, $"Lumi_Talking_{frame:D2}");
        }
        Debug.Log("💬 Talking animation: Expressive speech");
    }
    
    #endregion
    
    #region Core Drawing Methods - Cartoon Style
    
    void DrawCartoonFox(Texture2D tex, Vector2 offset, float breathe, float earAngle, 
                        float legOffset, float tailAngle, float eyeScale, 
                        bool showExcited = false, bool sadExpression = false, float mouthOpen = 0f)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2) + offset;
        
        // Drawing order (back to front)
        
        // 1. Tail (behind body)
        DrawCartoonTail(tex, center + new Vector2(-50, -60 + breathe), tailAngle);
        
        // 2. Back legs
        DrawCartoonLeg(tex, center + new Vector2(-35, -130 + legOffset), false);
        DrawCartoonLeg(tex, center + new Vector2(30, -130 - legOffset), false);
        
        // 3. Body
        DrawCartoonBody(tex, center + new Vector2(0, -40 + breathe));
        
        // 4. Front legs
        DrawCartoonLeg(tex, center + new Vector2(-55, -100 + legOffset * 0.5f), true);
        DrawCartoonLeg(tex, center + new Vector2(50, -100 - legOffset * 0.5f), true);
        
        // 5. Head (large proportion)
        DrawCartoonHead(tex, center + new Vector2(0, 80 + breathe));
        
        // 6. Ears
        DrawCartoonEar(tex, center + new Vector2(-55, 165 + breathe + earAngle), true);
        DrawCartoonEar(tex, center + new Vector2(55, 165 + breathe - earAngle), false);
        
        // 7. Facial features
        if (sadExpression)
        {
            DrawSadEyes(tex, center + new Vector2(0, 75 + breathe));
            DrawSadMouth(tex, center + new Vector2(0, 45 + breathe));
        }
        else if (mouthOpen > 0)
        {
            DrawOpenEyes(tex, center + new Vector2(0, 75 + breathe), eyeScale);
            DrawTalkingMouth(tex, center + new Vector2(0, 45 + breathe), mouthOpen);
        }
        else
        {
            DrawHappyEyes(tex, center + new Vector2(0, 75 + breathe), eyeScale);
            DrawHappyMouth(tex, center + new Vector2(0, 45 + breathe), showExcited);
        }
        
        DrawCartoonNose(tex, center + new Vector2(0, 58 + breathe));
        DrawCartoonCheeks(tex, center + new Vector2(0, 60 + breathe));
    }
    
    void DrawCartoonHead(Texture2D tex, Vector2 center)
    {
        // Large rounded head with outline
        int headWidth = 100;
        int headHeight = 95;
        
        // Fill
        DrawEllipse(tex, center, headWidth, headHeight, bodyColor);
        
        // White face marking (heart-shaped)
        DrawEllipse(tex, center + new Vector2(0, -15), 70, 75, accentColor);
        
        // Black outline
        DrawEllipseOutline(tex, center, headWidth + (int)outlineThickness, headHeight + (int)outlineThickness, outlineColor, (int)outlineThickness);
    }
    
    void DrawCartoonBody(Texture2D tex, Vector2 center)
    {
        int bodyWidth = 75;
        int bodyHeight = 85;
        
        // Main body
        DrawEllipse(tex, center, bodyWidth, bodyHeight, bodyColor);
        
        // White chest
        DrawEllipse(tex, center + new Vector2(0, 5), 50, 65, accentColor);
        
        // Body outline
        DrawEllipseOutline(tex, center, bodyWidth + (int)outlineThickness, bodyHeight + (int)outlineThickness, outlineColor, (int)outlineThickness);
    }
    
    void DrawCartoonEar(Texture2D tex, Vector2 basePos, bool isLeft)
    {
        // Large triangular ear
        Vector2 tip = basePos + new Vector2(0, 50);
        Vector2 leftBase = basePos + new Vector2(isLeft ? -28 : -22, -5);
        Vector2 rightBase = basePos + new Vector2(isLeft ? 22 : 28, -5);
        
        // Ear fill
        FillTriangle(tex, tip, leftBase, rightBase, bodyColor);
        
        // Inner ear (darker)
        Vector2 innerTip = basePos + new Vector2(0, 35);
        Vector2 innerLeft = basePos + new Vector2(isLeft ? -15 : -12, 0);
        Vector2 innerRight = basePos + new Vector2(isLeft ? 12 : 15, 0);
        FillTriangle(tex, innerTip, innerLeft, innerRight, darkOrangeColor);
        
        // Ear outline
        DrawTriangleOutline(tex, tip, leftBase, rightBase, outlineColor, (int)outlineThickness);
    }
    
    void DrawHappyEyes(Texture2D tex, Vector2 center, float scale)
    {
        // Closed happy eyes (^ ^)
        int eyeWidth = (int)(25 * scale);
        int eyeHeight = (int)(3 * scale);
        
        // Left eye (curved up)
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float t = (float)i / (eyeWidth/2);
            float curve = -t * t * eyeHeight * 2;
            Vector2 pos = center + new Vector2(-30 + i, curve);
            DrawCircle(tex, pos, (int)(outlineThickness * 0.8f), eyeColor);
        }
        
        // Right eye (curved up)
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float t = (float)i / (eyeWidth/2);
            float curve = -t * t * eyeHeight * 2;
            Vector2 pos = center + new Vector2(30 + i, curve);
            DrawCircle(tex, pos, (int)(outlineThickness * 0.8f), eyeColor);
        }
    }
    
    void DrawOpenEyes(Texture2D tex, Vector2 center, float scale)
    {
        int eyeSize = (int)(16 * scale);
        
        // Left eye
        DrawCircle(tex, center + new Vector2(-30, 0), eyeSize, Color.white);
        DrawCircle(tex, center + new Vector2(-28, -2), (int)(eyeSize * 0.6f), eyeColor);
        DrawCircle(tex, center + new Vector2(-32, 2), (int)(eyeSize * 0.25f), Color.white); // Shine
        DrawCircleOutline(tex, center + new Vector2(-30, 0), eyeSize, outlineColor, 2);
        
        // Right eye
        DrawCircle(tex, center + new Vector2(30, 0), eyeSize, Color.white);
        DrawCircle(tex, center + new Vector2(28, -2), (int)(eyeSize * 0.6f), eyeColor);
        DrawCircle(tex, center + new Vector2(26, 2), (int)(eyeSize * 0.25f), Color.white); // Shine
        DrawCircleOutline(tex, center + new Vector2(30, 0), eyeSize, outlineColor, 2);
    }
    
    void DrawSadEyes(Texture2D tex, Vector2 center)
    {
        // Downturned eyes
        int eyeWidth = 20;
        int eyeHeight = 3;
        
        // Left eye (curved down)
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float t = (float)i / (eyeWidth/2);
            float curve = t * t * eyeHeight * 2;
            Vector2 pos = center + new Vector2(-30 + i, curve);
            DrawCircle(tex, pos, 2, eyeColor);
        }
        
        // Right eye (curved down)
        for (int i = -eyeWidth/2; i <= eyeWidth/2; i++)
        {
            float t = (float)i / (eyeWidth/2);
            float curve = t * t * eyeHeight * 2;
            Vector2 pos = center + new Vector2(30 + i, curve);
            DrawCircle(tex, pos, 2, eyeColor);
        }
    }
    
    void DrawCartoonNose(Texture2D tex, Vector2 pos)
    {
        // Small black triangle nose
        Vector2 top = pos;
        Vector2 left = pos + new Vector2(-6, 8);
        Vector2 right = pos + new Vector2(6, 8);
        
        FillTriangle(tex, top, left, right, noseColor);
    }
    
    void DrawHappyMouth(Texture2D tex, Vector2 center, bool showTongue)
    {
        // Wide smile
        int mouthWidth = 35;
        
        // Draw smile curve
        for (int i = -mouthWidth; i <= mouthWidth; i++)
        {
            float t = (float)i / mouthWidth;
            float curve = -t * t * 15f;
            Vector2 pos = center + new Vector2(i, curve - 8);
            DrawCircle(tex, pos, (int)(outlineThickness * 0.9f), outlineColor);
        }
        
        // Optional tongue
        if (showTongue)
        {
            DrawEllipse(tex, center + new Vector2(0, -15), 12, 18, tongueColor);
            DrawEllipseOutline(tex, center + new Vector2(0, -15), 12, 18, outlineColor, 2);
        }
    }
    
    void DrawSadMouth(Texture2D tex, Vector2 center)
    {
        // Downturned mouth
        int mouthWidth = 25;
        
        for (int i = -mouthWidth; i <= mouthWidth; i++)
        {
            float t = (float)i / mouthWidth;
            float curve = t * t * 8f;
            Vector2 pos = center + new Vector2(i, curve - 8);
            DrawCircle(tex, pos, 2, outlineColor);
        }
    }
    
    void DrawTalkingMouth(Texture2D tex, Vector2 center, float openAmount)
    {
        // Oval mouth when talking
        int mouthWidth = (int)(15 + openAmount * 12);
        int mouthHeight = (int)(8 + openAmount * 15);
        
        DrawEllipse(tex, center + new Vector2(0, -8), mouthWidth, mouthHeight, Color.black);
        DrawEllipseOutline(tex, center + new Vector2(0, -8), mouthWidth, mouthHeight, outlineColor, 2);
        
        // Tongue visible when more open
        if (openAmount > 0.5f)
        {
            DrawEllipse(tex, center + new Vector2(0, -12), 8, 10, tongueColor);
        }
    }
    
    void DrawCartoonCheeks(Texture2D tex, Vector2 center)
    {
        // Rosy blush marks
        DrawEllipse(tex, center + new Vector2(-55, -5), 14, 10, blushColor, 0f, 0.7f);
        DrawEllipse(tex, center + new Vector2(55, -5), 14, 10, blushColor, 0f, 0.7f);
    }
    
    void DrawCartoonLeg(Texture2D tex, Vector2 pos, bool isFront)
    {
        // Rounded leg
        DrawEllipse(tex, pos, 22, 35, bodyColor);
        
        // Paw pad (darker)
        DrawEllipse(tex, pos + new Vector2(0, -8), 16, 12, darkOrangeColor);
        
        // Outline
        DrawEllipseOutline(tex, pos, 22 + (int)outlineThickness, 35 + (int)outlineThickness, outlineColor, (int)outlineThickness);
    }
    
    void DrawCartoonTail(Texture2D tex, Vector2 pos, float angle)
    {
        // Fluffy tail
        Vector2 tailPos = pos + new Vector2(angle * 0.4f, angle * 0.2f);
        
        // Main tail body
        DrawEllipse(tex, tailPos, 48, 32, bodyColor, -35f + angle);
        
        // White tip
        DrawCircle(tex, tailPos + RotatePoint(new Vector2(-32, -32), Vector2.zero, -35f + angle), 18, accentColor);
        
        // Outline
        DrawEllipseOutline(tex, tailPos, 48 + (int)outlineThickness, 32 + (int)outlineThickness, outlineColor, (int)outlineThickness, -35f + angle);
        DrawCircleOutline(tex, tailPos + RotatePoint(new Vector2(-32, -32), Vector2.zero, -35f + angle), 18, outlineColor, (int)outlineThickness);
    }
    
    void DrawSleepingCartoonFox(Texture2D tex, float breathe)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2 - 20);
        
        // Curled up body
        DrawEllipse(tex, center, 120, 85, bodyColor, 10f);
        DrawEllipse(tex, center + new Vector2(10, 0), 85, 60, accentColor, 10f);
        
        // Curled tail over
        DrawEllipse(tex, center + new Vector2(-35, 35), 60, 38, bodyColor, -55f);
        DrawCircle(tex, center + new Vector2(-50, 30), 16, accentColor);
        
        // Head tucked
        DrawEllipse(tex, center + new Vector2(45, 15), 65, 70, bodyColor);
        DrawEllipse(tex, center + new Vector2(50, 5), 45, 50, accentColor);
        
        // Small ears
        DrawCartoonEar(tex, center + new Vector2(25, 60), true);
        DrawCartoonEar(tex, center + new Vector2(65, 60), false);
        
        // Closed eyes (sleeping)
        for (int i = -12; i <= 12; i++)
        {
            Vector2 pos = center + new Vector2(35 + i, 20 - i * i * 0.02f);
            DrawCircle(tex, pos, 2, eyeColor);
        }
        for (int i = -12; i <= 12; i++)
        {
            Vector2 pos = center + new Vector2(60 + i, 20 - i * i * 0.02f);
            DrawCircle(tex, pos, 2, eyeColor);
        }
        
        // Small nose
        DrawCartoonNose(tex, center + new Vector2(48, 10));
        
        // Add outlines
        DrawEllipseOutline(tex, center, 120, 85, outlineColor, (int)outlineThickness, 10f);
        DrawEllipseOutline(tex, center + new Vector2(45, 15), 65, 70, outlineColor, (int)outlineThickness);
    }
    
    #endregion
    
    #region Effect Methods
    
    void DrawHappySparkles(Texture2D tex, int frame)
    {
        System.Random rand = new System.Random(frame);
        for (int i = 0; i < 6; i++)
        {
            Vector2 pos = new Vector2(rand.Next(80, spriteSize - 80), rand.Next(80, spriteSize - 80));
            DrawStar(tex, pos, 6 + rand.Next(4), frame * 45f + i * 30f);
        }
    }
    
    void DrawCelebrationEffects(Texture2D tex, int frame)
    {
        System.Random rand = new System.Random(frame * 2);
        
        // Stars
        for (int i = 0; i < 10; i++)
        {
            Vector2 pos = new Vector2(rand.Next(50, spriteSize - 50), rand.Next(50, spriteSize - 50));
            DrawStar(tex, pos, 5 + rand.Next(3), frame * 60f);
        }
        
        // Confetti
        for (int i = 0; i < 12; i++)
        {
            Vector2 pos = new Vector2(rand.Next(0, spriteSize), rand.Next(0, spriteSize));
            Color color = new Color(rand.Next(50, 100) / 100f, rand.Next(50, 100) / 100f, rand.Next(50, 100) / 100f);
            DrawCircle(tex, pos, 4, color);
        }
    }
    
    void DrawMotionLines(Texture2D tex)
    {
        Vector2 center = new Vector2(spriteSize / 2, spriteSize / 2);
        for (int i = 0; i < 3; i++)
        {
            Vector2 start = center + new Vector2(-70 - i * 15, 30 - i * 8);
            Vector2 end = start + new Vector2(-25, 3);
            DrawThickLine(tex, start, end, 2, new Color(1f, 1f, 1f, 0.6f));
        }
    }
    
    void DrawThoughtBubble(Texture2D tex, int frame)
    {
        if (frame % 3 == 0)
        {
            Vector2 pos = new Vector2(spriteSize / 2 + 90, spriteSize / 2 + 140);
            
            // Main bubble
            DrawCircle(tex, pos, 25, Color.white);
            DrawCircleOutline(tex, pos, 25, outlineColor, 2);
            
            // Small connecting bubbles
            DrawCircle(tex, pos + new Vector2(-15, -18), 8, Color.white);
            DrawCircleOutline(tex, pos + new Vector2(-15, -18), 8, outlineColor, 2);
            
            DrawCircle(tex, pos + new Vector2(-25, -28), 5, Color.white);
            DrawCircleOutline(tex, pos + new Vector2(-25, -28), 5, outlineColor, 2);
        }
    }
    
    void DrawEncouragementHearts(Texture2D tex, int frame)
    {
        if (frame % 2 == 0)
        {
            Vector2 pos = new Vector2(spriteSize / 2 - 75, spriteSize / 2 + 110);
            DrawHeart(tex, pos, 14);
            
            if (frame % 4 == 0)
            {
                DrawHeart(tex, pos + new Vector2(30, 20), 10);
            }
        }
    }
    
    void DrawHeart(Texture2D tex, Vector2 center, int size)
    {
        // Two circles for top of heart
        DrawCircle(tex, center + new Vector2(-size / 3, size / 3), size / 2, new Color(1f, 0.3f, 0.4f, 0.85f));
        DrawCircle(tex, center + new Vector2(size / 3, size / 3), size / 2, new Color(1f, 0.3f, 0.4f, 0.85f));
        
        // Triangle for bottom
        FillTriangle(tex, 
            center + new Vector2(-size / 2, size / 4), 
            center + new Vector2(size / 2, size / 4), 
            center + new Vector2(0, -size), 
            new Color(1f, 0.3f, 0.4f, 0.85f));
        
        // Outline
        DrawCircleOutline(tex, center + new Vector2(-size / 3, size / 3), size / 2, new Color(0.8f, 0.2f, 0.3f), 2);
        DrawCircleOutline(tex, center + new Vector2(size / 3, size / 3), size / 2, new Color(0.8f, 0.2f, 0.3f), 2);
    }
    
    void DrawSleepingZ(Texture2D tex, int frame)
    {
        float offset = frame * 12f;
        Vector2 basePos = new Vector2(spriteSize / 2 + 95, spriteSize / 2 + 140 - offset);
        
        if (offset < 90)
        {
            DrawZLetter(tex, basePos, 16);
            if (offset > 25) DrawZLetter(tex, basePos + new Vector2(18, 28), 13);
            if (offset > 50) DrawZLetter(tex, basePos + new Vector2(36, 56), 10);
        }
    }
    
    void DrawZLetter(Texture2D tex, Vector2 pos, int size)
    {
        Color zColor = new Color(0.5f, 0.6f, 0.9f, 0.75f);
        
        // Top horizontal line
        DrawThickLine(tex, pos, pos + new Vector2(size, 0), 3, zColor);
        
        // Diagonal line
        DrawThickLine(tex, pos + new Vector2(size, 0), pos + new Vector2(0, size), 3, zColor);
        
        // Bottom horizontal line
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
        
        Color starColor = new Color(1f, 0.85f, 0.2f); // Gold color
        
        for (int i = 0; i < 10; i++)
        {
            FillTriangle(tex, center, points[i], points[(i + 1) % 10], starColor);
        }
        
        // Star center highlight
        DrawCircle(tex, center, size / 3, Color.white);
    }
    
    #endregion
    
    #region Utility Drawing Methods
    
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
    
    Vector2[] RotatePoints(Vector2[] points, Vector2 pivot, float angle)
    {
        Vector2[] rotated = new Vector2[points.Length];
        float rad = angle * Mathf.Deg2Rad;
        
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 dir = points[i] - pivot;
            rotated[i] = pivot + new Vector2(
                dir.x * Mathf.Cos(rad) - dir.y * Mathf.Sin(rad),
                dir.x * Mathf.Sin(rad) + dir.y * Mathf.Cos(rad)
            );
        }
        return rotated;
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