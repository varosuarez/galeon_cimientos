using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobController : MonoBehaviour
{

    private int num = 1000;
    private NativeArray<int> result;
    private FibonacciJob fib;
    private JobHandle handle;
    private JobHandle Secondhandle;
    private bool init = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //LaunchJob();
            LaunchJobParallel();
        }

        if (init)
        {
            if (Secondhandle.IsCompleted)
            {
                Secondhandle.Complete();
                init = false;
                for (int i = 0; i < num; ++i)
                {
                    Debug.Log(result[i]);
                }
                result.Dispose();
            }
        }
    }

    protected void LaunchJobParallel()
    {
        init = true;
        result = new NativeArray<int>(num, Allocator.Persistent);
        FibonacciJob fibJob = new FibonacciJob(num, ref result);
        CalcWithFinbonacciParallel calcWitJob = new
        CalcWithFinbonacciParallel(num, ref result);
        handle = fibJob.Schedule();
        Secondhandle = calcWitJob.Schedule(num, 100, handle);
    }

    protected void LaunchJob()
    {
        init = true;
        result = new NativeArray<int>(num, Allocator.Persistent);
        FibonacciJob fibJob = new FibonacciJob(num, ref result);
        CalcWithFibonacciJob SecondfibJob = new CalcWithFibonacciJob(num, ref result);
        handle = fibJob.Schedule();
        Secondhandle = SecondfibJob.Schedule(handle);
    }
}
