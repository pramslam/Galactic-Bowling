using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class AudioManagerTest
{
    private int lastStanding, standing;
    private AudioManager audioManager;

    private AudioManager.Sound strike = AudioManager.Sound.Strike;
    private AudioManager.Sound spare = AudioManager.Sound.Spare;
    private AudioManager.Sound gutter = AudioManager.Sound.Gutter;

    [SetUp]
    public void Setup()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01TenStandingGutter()
    {
        lastStanding = 10;
        standing = 10;

        Assert.AreEqual(gutter, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T02TenStandingStrike()
    {
        lastStanding = 10;
        standing = 0;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T03TenStandingOneHit()
    {
        lastStanding = 10;
        standing = 9;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T04TenStandingThreeHit()
    {
        lastStanding = 10;
        standing = 7;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T05TenStandingFourHit()
    {
        lastStanding = 10;
        standing = 6;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T06TenStandingNineHit()
    {
        lastStanding = 10;
        standing = 6;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T07NineStandingOneHit()
    {
        lastStanding = 9;
        standing = 8;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T08NineStandingThreeHit()
    {
        lastStanding = 9;
        standing = 6;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T09NineStandingFourHit()
    {
        lastStanding = 9;
        standing = 5;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T10NineStandingEightHit()
    {
        lastStanding = 9;
        standing = 1;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T11NineStandingSpare()
    {
        lastStanding = 9;
        standing = 0;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T12SixStandingGutter()
    {
        lastStanding = 6;
        standing = 6;

        Assert.AreEqual(gutter, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T13SixStandingThreeHit()
    {
        lastStanding = 6;
        standing = 3;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T14SixStandingFourHit()
    {
        lastStanding = 6;
        standing = 2;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T15SixStandingSpare()
    {
        lastStanding = 6;
        standing = 0;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T16FiveStandingSpare()
    {
        lastStanding = 5;
        standing = 0;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T17FiveStandingFourHit()
    {
        lastStanding = 5;
        standing = 1;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T18FiveStandingThreeHit()
    {
        lastStanding = 5;
        standing = 2;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T19FiveStandingTwoHit()
    {
        lastStanding = 5;
        standing = 3;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T20FourStandingSpare()
    {
        lastStanding = 4;
        standing = 0;

        Assert.AreEqual(strike, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T21FourStandingThreeHit()
    {
        lastStanding = 4;
        standing = 1;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T22FourStandingOneHit()
    {
        lastStanding = 4;
        standing = 3;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T23ThreeStandingSpare()
    {
        lastStanding = 3;
        standing = 0;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T24ThreeStandingTwoHit()
    {
        lastStanding = 3;
        standing = 1;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T25ThreeStandingGutter()
    {
        lastStanding = 3;
        standing = 3;

        Assert.AreEqual(gutter, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T26TwoStandingSpare()
    {
        lastStanding = 2;
        standing = 0;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T27OneStandingSpare()
    {
        lastStanding = 1;
        standing = 0;

        Assert.AreEqual(spare, audioManager.ProcessAudio(lastStanding, standing));
    }

    [Test]
    public void T28OneStandingGutter()
    {
        lastStanding = 1;
        standing = 1;

        Assert.AreEqual(gutter, audioManager.ProcessAudio(lastStanding, standing));
    }
}