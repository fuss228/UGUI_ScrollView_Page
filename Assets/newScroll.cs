using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class newScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    ScrollRect rect;

    //页面：0，1，2  索引从0开始
    //每夜占的比列：0/2=0  1/2=0.5  2/2=1
    float[] page = { 1, 0.66f,0.33f, 0 };

    //滑动速度
    public float smooting = 10;

    //滑动的起始坐标
    float targethorizontal = 0;

    //是否拖拽结束
    bool isDrag = true;


    float startBeginDragY;

    int currentPageIndex = 0;
    // Use this for initialization
    void Start()
    {
        rect = transform.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果不判断。当在拖拽的时候要也会执行插值，所以会出现闪烁的效果
        //这里只要在拖动结束的时候。在进行插值
        if (!isDrag)
        {
            rect.verticalNormalizedPosition = Mathf.Lerp(rect.verticalNormalizedPosition, targethorizontal, Time.deltaTime * smooting);
        }
    }

    /// <summary>
    /// 拖动开始
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        startBeginDragY = rect.verticalNormalizedPosition;

        //Debug.Log("start:"+startBeginDragY);
    }

    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        //    //拖动停止滑动的坐标 
        //    Vector2 f = rect.normalizedPosition;
        //    //水平  开始值是0  结尾值是1  [0,1]
        //    float h = rect.horizontalNormalizedPosition;
        //    //垂直
        //    float v = rect.verticalNormalizedPosition;




        float posX = rect.verticalNormalizedPosition;


        Debug.Log("chazhi = "+(startBeginDragY-posX));

        float offset = startBeginDragY - posX;
        if (offset >=0.01f) //下一页
        {

            if (currentPageIndex == 3)
            {
                return;
            }
            currentPageIndex++;
        }
        else if(offset <= -0.01f)//上一页
        {
            if (currentPageIndex == 0)
            {
                return;
            }
            currentPageIndex--;
        }




        //int index = 0;
        ////假设离第一位最近
        //float offset = Mathf.Abs(page[index] - posX);
        //for (int i = 1; i < page.Length; i++)
        //{
        //    float temp = Mathf.Abs(page[i] - posX);
        //    if (temp < offset)
        //    {
        //        index = i;

        //        //保存当前的偏移量
        //        //如果到最后一页。反翻页。所以要保存该值，如果不保存。你试试效果就知道
        //        offset = temp;
        //    }
        //}

        /*
         因为这样效果不好。没有滑动效果。比较死板。所以改为插值
         */
        //rect.horizontalNormalizedPosition = page[index];

        Debug.LogError("page:"+currentPageIndex);
        targethorizontal = page[currentPageIndex];
    }
}