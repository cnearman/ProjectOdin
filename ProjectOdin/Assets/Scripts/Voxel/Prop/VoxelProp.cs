using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class VoxelProp : MonoBehaviour
{
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];

    public static int chunkSize = 16;
    public bool update = false;

    MeshFilter filter;
    MeshCollider coll;

   

    public bool rendered;


    // Use this for initialization
    void Start()
    {

        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();
        MakeTree();
        SetBlocksUnmodified();
        update = true;

    }

    
    //only works for true spheres, none of that oval bs
    public void TakeSphereDamage(GameObject damage)
    {
        Debug.Log("boomSphere");
        //this is the worst, completely unoptimised i just want it to fucking work version
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    Vector3 posCheck = x * transform.right + y * transform.up + z * transform.forward + transform.position;

                    Vector3 pointToCenter = damage.transform.position - posCheck;

                    

                    if(pointToCenter.magnitude < damage.transform.localScale.y / 2f)
                    {
                        //Debug.Log(pointToCenter.magnitude);
                        SetBlock(x, y, z, new BlockAir());
                    }
                }
            }
        }

        update = true;

    }

    //currently only works for axis alined rectangular prisms cause math is hard
    public void TakeRectangularDamage(GameObject damage)
    {
        Debug.Log("boom");
        //this is the worst, completely unoptimised i just want it to fucking work version
       


        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    Vector3 posCheck = x * transform.right + y * transform.up + z * transform.forward + transform.position;
                    
                    if (posCheck.x > damage.transform.position.x - (damage.transform.localScale.x / 2) && posCheck.x <  damage.transform.position.x + (damage.transform.localScale.x / 2))
                    {
                        if (posCheck.y > damage.transform.position.y - (damage.transform.localScale.y / 2) && posCheck.y < damage.transform.position.y + (damage.transform.localScale.y / 2))
                        {
                            if (posCheck.z > damage.transform.position.z - (damage.transform.localScale.z / 2) && posCheck.z < damage.transform.position.z + (damage.transform.localScale.z / 2))
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
                        SetBlock(7+xi, yi, 7+zi, new BlockLime());
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

}
