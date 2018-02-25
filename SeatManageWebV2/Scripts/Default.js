function onReady() {
    var v_toolbar = Ext.getCmp(IDS.toolbar);
    var v_treeMenu = Ext.getCmp(IDS.treeMenu);
    var v_mainTabStrip = Ext.getCmp(IDS.mainTabStrip);
    var btnExpandAll = Ext.getCmp(IDS.btnExpandAll);
    var btnCollapseAll = Ext.getCmp(IDS.btnCollapseAll);

    var treeMenu = v_treeMenu;


    // 点击树节点
    treeMenu.on('click', function (node, event) {
        if (node.isLeaf()) {
            // 阻止事件传播
            event.stopEvent();
            var href = node.attributes.href;
            //window.location.href =href;
            addExampleTab(node);
        }
    });

    // 动态添加一个标签页
    function addExampleTab(node) {
        var href = node.attributes.href;
        // Add a dynamic tab (With toolbar).
        var mainTabStrip = v_mainTabStrip;
        var tabID = 'dynamic_added_tab' + node.id.replace('__', '-');
        var currentTab = mainTabStrip.getTab(tabID);
        if (!currentTab) {
            mainTabStrip.addTab({
                'id': tabID,
                'url': href,
                'title': node.text,
                'closable': true,
                'bodyStyle': 'padding:0px;',
                'iconCls': 'icon_aspx'
            });
        } else {
            mainTabStrip.setActiveTab(currentTab);
        }

    }

    // 点击全部展开按钮
    btnExpandAll.on('click', function () {
        treeMenu.expandAll();
    });

    // 点击全部折叠按钮
    btnCollapseAll.on('click', function () {
        treeMenu.collapseAll();
    });

    //alert(v_toolbar.type);
    //v_toolbar.items.add(btn);

    window.addExampleTab = addExampleTab;
}