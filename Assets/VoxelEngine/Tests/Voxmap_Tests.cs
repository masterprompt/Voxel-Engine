﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using VoxelEngine;

public class Voxmap_Tests
{
    private Chunk GetVoxmap()
    {
        var voxmap = new Chunk(3,3,3);
        voxmap[0,1,2] = Voxel.Create(1);
        voxmap[2,1,0] = Voxel.Create(2);
        voxmap[0,2,1] = Voxel.Create(3);
        return voxmap;
    }
    
    [Test]
    public void TestXLocation() {
        Assert.AreEqual(GetVoxmap()[0,1,2].material, 1);
    }

    [Test]
    public void TestYLocation() {
        Assert.AreEqual(GetVoxmap()[2,1,0].material, 2);
    }
    
    [Test]
    public void TestZLocation() {
        Assert.AreEqual(GetVoxmap()[0,2,1].material, 3);
    }
    
    [Test]
    public void TestSize1()
    {
        var voxmap = new Chunk(1,1,1);
        Assert.AreEqual(voxmap.VoxelSize.x, 1f);
    }
    
    [Test]
    public void TestSize2()
    {
        var voxmap = new Chunk(2,2,2);
        Assert.AreEqual(voxmap.VoxelSize.x, 0.5f);
    }
    
    [Test]
    public void TestSize4()
    {
        var voxmap = new Chunk(4,4,4);
        Assert.AreEqual(voxmap.VoxelSize.x, 0.25f);
    }
}
