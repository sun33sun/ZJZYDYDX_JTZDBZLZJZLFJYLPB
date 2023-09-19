﻿using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using ProjectBase;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
    public class Main : PersistentMonoSingleton<Main>
    {
        async UniTask Start()
        {
            await ExtensionFunction.UnrecordOpenPanelAsync<TopPanel>(TopPanel.Name);
            await ExtensionFunction.UnrecordOpenPanelAsync<BottomPanel>(BottomPanel.Name);
        }
    }
}