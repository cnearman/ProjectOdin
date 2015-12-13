using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class VoxelProp : BaseClass
{
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];
    public int[,,] blockInt = new int[chunkSize, chunkSize, chunkSize];

    public static int chunkSize = 16;
    public bool update = false;

    MeshFilter filter;
    MeshCollider coll;



    public bool rendered;

    public string unique;


    public void BlockToInt()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    blockInt[x, y, z] = blocks[x, y, z].blockInt;
                }
            }
        }
    }

    public void IntToBlock()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (blockInt[x, y, z] == 0)
                    {
                        blocks[x, y, z] = new BlockAir();
                    }
                    else if (blockInt[x, y, z] == 1)
                    {
                        blocks[x, y, z] = new Block();
                    }
                    else if (blockInt[x, y, z] == 2)
                    {
                        blocks[x, y, z] = new BlockGrass();
                    }
                    else if (blockInt[x, y, z] == 10)
                    {
                        blocks[x, y, z] = new BlockBrown();
                    }
                    else if (blockInt[x, y, z] == 11)
                    {
                        blocks[x, y, z] = new BlockDarkBrown();
                    }
                    else if (blockInt[x, y, z] == 12)
                    {
                        blocks[x, y, z] = new BlockDarkGray();
                    }
                    else if (blockInt[x, y, z] == 13)
                    {
                        blocks[x, y, z] = new BlockGray();
                    }
                    else if (blockInt[x, y, z] == 14)
                    {
                        blocks[x, y, z] = new BlockGreen();
                    }
                    else if (blockInt[x, y, z] == 15)
                    {
                        blocks[x, y, z] = new BlockLime();
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {

        //gameObject.name = "Prop " + GetComponent<NetworkIdentity>().assetId.ToString();
        /*if (!isServer)
        {
            unique =  Guid.NewGuid().ToString();
            gameObject.name = unique;
        }*/

        gameObject.name = "Prop " + transform.position.ToString();

        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();
        //MakeTree();
        SetBlocksUnmodified();
        update = true;

    }


    //only works for true spheres, none of that oval bs
    public void TakeSphereDamage(GameObject damage)
    {
        GameObject.Find("PropDestruction").GetComponent<PropDestruction>().SphereDestroy(damage.transform.position, damage.transform.localScale.y, gameObject.name);
        //Debug.Log("boomSphere");
        //this is the worst, completely unoptimised i just want it to fucking work version
        /*for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    Vector3 posCheck = x * transform.right + y * transform.up + z * transform.forward + transform.position;

                    Vector3 pointToCenter = damage.transform.position - posCheck;



                    if (pointToCenter.magnitude < damage.transform.localScale.y / 2f)
                    {
                        //Debug.Log(pointToCenter.magnitude);
                        SetBlock(x, y, z, new BlockAir());
                    }
                }
            }
        }*/

        update = true;

    }

    

    public void TakeSphereDamageNet(Vector3 spherePos, float sphereScale)
    {

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    Vector3 posCheck = x * transform.right + y * transform.up + z * transform.forward + transform.position;

                    Vector3 pointToCenter = spherePos - posCheck;



                    if (pointToCenter.magnitude < sphereScale / 2f)
                    {
                        //Debug.Log(pointToCenter.magnitude);
                        SetBlock(x, y, z, new BlockAir());
                    }
                }
            }
        }

        update = true;
    }

    void MakeTree()
    {
        for (int xa = 0; xa < chunkSize; xa++)
        {
            for (int ya = 0; ya < chunkSize; ya++)
            {
                for (int za = 0; za < chunkSize; za++)
                {
                    SetBlock(xa, ya, za, new BlockAir());
                }
            }
        }

        //create leaves
        for (int xi = -2; xi <= 2; xi++)
        {
            for (int yi = 6; yi <= 10; yi++)
            {
                for (int zi = -2; zi <= 2; zi++)
                {
                    if (Mathf.Abs(zi) == 2 && (Mathf.Abs(yi) == 10 || Mathf.Abs(yi) == 6) && Mathf.Abs(xi) == 2)
                    {

                    }
                    else
                    {
                        SetBlock(7 + xi, yi, 7 + zi, new BlockLime());
                    }
                }
            }
        }

        //create trunk
        for (int yt = 0; yt < 8; yt++)
        {
            SetBlock(7, yt, 7, new BlockBrown());
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }

    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.Modified = false;
        }
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return new BlockAir();
    }

    //new function
    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {

        }
    }

    void UpdateChunk()
    {

        rendered = true;

        MeshData meshData = new MeshData();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (blocks[x, y, z].air)
                    {

                    }
                    else
                    {
                        meshData = BlockData(x, y, z, meshData);
                    }

                    //meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }

        RenderMesh(meshData);
    }

    MeshData BlockData(int x, int y, int z, MeshData meshData)
    {
        meshData.useRenderDataForCol = true;



        if (y == chunkSize - 1 || !GetBlock(x, y + 1, z).IsSolid(Direction.Down))
        {
            meshData = GetBlock(x, y, z).FaceDataUp(x, y, z, meshData);
        }

        if (y == 0 || !GetBlock(x, y - 1, z).IsSolid(Direction.Up))
        {
            meshData = GetBlock(x, y, z).FaceDataDown(x, y, z, meshData);
        }

        if (z == chunkSize - 1 || !GetBlock(x, y, z + 1).IsSolid(Direction.South))
        {
            meshData = GetBlock(x, y, z).FaceDataNorth(x, y, z, meshData);
        }

        if (z == 0 || !GetBlock(x, y, z - 1).IsSolid(Direction.North))
        {
            meshData = GetBlock(x, y, z).FaceDataSouth(x, y, z, meshData);
        }

        if (x == chunkSize - 1 || !GetBlock(x + 1, y, z).IsSolid(Direction.Left))
        {
            meshData = GetBlock(x, y, z).FaceDataLeft(x, y, z, meshData);
        }

        if (x == 0 || !GetBlock(x - 1, y, z).IsSolid(Direction.Right))
        {
            meshData = GetBlock(x, y, z).FaceDataRight(x, y, z, meshData);
        }

        return meshData;
    }

    // Sends the calculated mesh information
    // to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
        filter.mesh.uv = meshData.uv.ToArray();

        filter.mesh.RecalculateNormals();

        //additions:
        coll.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        coll.sharedMesh = mesh;
    }



    //when i learn math better i'll try to fix this. it needs to be changed anyway to be optimized
    public void TakeRectangularDamage(GameObject damage)
    {
        Debug.Log("boom");
        //this is the worst, completely unoptimised i just want it to fucking work version

        //possible devide by 0 error, but i try fixes, may be broken in certain orientations

        //sooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo shit is dumb so i made some jankey ass adjustments

        Vector3 r = damage.transform.right;
        Vector3 u = damage.transform.up;
        Vector3 f = damage.transform.forward;

        Vector4 temprow1 = new Vector4(r.x, u.x, f.x, damage.transform.position.x);
        Vector4 temprow2 = new Vector4(r.y, u.y, f.y, damage.transform.position.y);
        Vector4 temprow3 = new Vector4(r.z, u.z, f.z, damage.transform.position.z);

        Debug.Log(temprow1);
        Debug.Log(temprow2);
        Debug.Log(temprow3);

      
        //now we change the point to a different basis


        Vector4 row1 = temprow1;
        Vector4 row2 = temprow2;
        Vector4 row3 = temprow3;

        //reduce x
        if (Mathf.Abs(row1.x) < 0.05f)
        {
            if (row2.x == 0f)
            {
                //swap 1 and 3
                //Vector3 tempy = row1;
                //row1 = row3;
                //row3 = tempy;

                row1 = row1 + row3;
            }
            else
            {
                //swap 1 and 2
                //Vector3 tempy = row1;
                //row1 = row2;
                //row2 = tempy;

                row1 = row1 + row2;
            }
        }
        else
        {
            //all good
            //Debug.Log(row1.x + " all good");
        }
        row1 = row1 / row1.x;
        row2 -= row1 * row2.x;
        row3 -= row1 * row3.x;

        //reduce y
        if (Mathf.Abs(row2.y) < 0.05f)
        {
            //Vector3 tempy = row2;
            //row2 = row3;
            //row3 = tempy;

            row2 = row2 + row3;
        }
        else
        {
            //all good
            //Debug.Log("all good");
        }
        row2 = row2 / row2.y;
        row1 -= row2 * row1.y;
        row3 -= row2 * row3.y;

        //ruduce z
        if (row3.z == 0f)
        {
            Debug.Log("rectangular prism destuction error");
        }
        row3 = row3 / row3.z;
        row1 -= row3 * row1.z;
        row2 -= row3 * row2.z;

        Vector3 dPos = new Vector3(row1.w, row2.w, row3.w);
        Debug.Log(dPos);


        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    //first we get the world position of the voxel
                    Vector3 posCheck = x * transform.right + y * transform.up + z * transform.forward + transform.position;

                    temprow1 = new Vector4(r.x, u.x, f.x, posCheck.x);
                    temprow2 = new Vector4(r.y, u.y, f.y, posCheck.y);
                    temprow3 = new Vector4(r.z, u.z, f.z, posCheck.z);



                    //make da augmented
                    row1 = temprow1;
                    row2 = temprow2;
                    row3 = temprow3;

                    //reduce x
                    if (Mathf.Abs(row1.x) < 0.05f)
                    {
                        if (row2.x == 0f)
                        {
                            //swap 1 and 3
                            //Vector3 tempy = row1;
                            //row1 = row3;
                            //row3 = tempy;

                            row1 = row1 + row3;
                        }
                        else
                        {
                            //swap 1 and 2
                            //Vector3 tempy = row1;
                            //row1 = row2;
                            //row2 = tempy;

                            row1 = row1 + row2;
                        }
                    }
                    else
                    {
                        //all good
                        //Debug.Log(row1.x + " all good");
                    }
                    row1 = row1 / row1.x;
                    row2 -= row1 * row2.x;
                    row3 -= row1 * row3.x;

                    //reduce y
                    if (Mathf.Abs(row2.y) < 0.05f)
                    {
                        //Vector3 tempy = row2;
                        //row2 = row3;
                        //row3 = tempy;

                        row2 = row2 + row3;
                    }
                    else
                    {
                        //all good
                        //Debug.Log("all good");
                    }
                    row2 = row2 / row2.y;
                    row1 -= row2 * row1.y;
                    row3 -= row2 * row3.y;

                    //ruduce z
                    if (row3.z == 0f)
                    {
                        Debug.Log("rectangular prism destuction error");
                    }
                    row3 = row3 / row3.z;
                    row1 -= row3 * row1.z;
                    row2 -= row3 * row2.z;

                    Vector3 truPos = new Vector3(row1.w, row2.w, row3.w);
                    //=====================================================



                    if (truPos.x > dPos.x - (damage.transform.localScale.x / 2) && truPos.x < dPos.x + (damage.transform.localScale.x / 2))
                    {
                        if (truPos.y > dPos.y - (damage.transform.localScale.y / 2) && truPos.y < dPos.y + (damage.transform.localScale.y / 2))
                        {
                            if (truPos.z > dPos.z - (damage.transform.localScale.z / 2) && truPos.z < dPos.z + (damage.transform.localScale.z / 2))
                            {
                                //Debug.Log("hit");
                                SetBlock(x, y, z, new BlockAir());
                            }
                        }
                    }
                }
            }
        }

        update = true;
    }
}
