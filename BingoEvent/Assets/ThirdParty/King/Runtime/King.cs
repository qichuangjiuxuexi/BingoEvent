using UnityEngine;

public class King : Singleton<King>
{
    public KingAssetNode LoadAsset(string assetPathFileName)
    {
        KingAssetNode assetNode = new KingAssetNode();
        assetNode.assetPathFilename = assetPathFileName;
        assetNode.assetObject = Resources.Load(assetPathFileName);

        return assetNode;
    }

    public KingAssetNode LoadSprite(string assetPathFileName)
    {
        KingAssetNode assetNode = new KingAssetNode();
        assetNode.assetPathFilename = assetPathFileName;
        assetNode.assetObject = Resources.Load<Sprite>(assetPathFileName);

        return assetNode;
    }
}