$(document).ready(function () {
    var JustBlog = {};

    JustBlog.GridManager = {};

      //************************* POSTS GRID
    JustBlog.GridManager.postsGrid = function (gridName, pagerName) {

        //*** Event handlers
        var afterShowForm = function (form) {
            console.log("afterShowForm")
            tinyMCE.init('mceAddControl', false, "ShortDescription");
            tinyMCE.init('mceAddControl', false, "Description");
        };

        var onClose = function (form) {
            console.log("onClose")
            tinyMCE.execCommand('mceRemoveControl', false, "ShortDescription");
            tinyMCE.execCommand('mceRemoveControl', false, "Description");
        };


        // columns
        var colNames = [
            'Id',
            'Title',
            'Short Description',
            'Description',
            'Category',
            'Category',
            'Tags',
            'Meta',
            'Url Slug',
            'Published',
            'Posted On',
            'Modified'
        ];

        var columns = [];

        columns.push({
            name: 'Id',
            hidden: true,
            key: true
        });

        columns.push({
            name: 'Title',
            index: 'Title',
            width: 250,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 500
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'ShortDescription',
            index: 'ShortDescription',
            width: 250,
            sortable: false,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "10",
                cols: "100"
            },
            editrules: {
                custom: true,
                custom_func: function (val, colname) {
                    val = tinyMCE.get("ShortDescription").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Field is required"];
                },
                edithidden: true
            }
        });

        columns.push({
            name: 'Description',
            width: 250,
            sortable: false,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "40",
                cols: "100"
            },
            editrules: {
                edithidden: true
            }
        });

        columns.push({
            name: 'Category.Id',
            hidden: true,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                //dataUrl: '/Admin/GetCategoriesHtml'
            },
            editrules: {
                required: true,
                edithidden: true
            }
        });

        columns.push({
            name: 'Category.Name',
            index: 'Category',
            width: 150
        });


        columns.push({
            name: 'Tags',
            width: 150,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                //dataUrl: '/Admin/GetTagsHtml',
                multiple: true
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'Meta',
            width: 250,
            sortable: false,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "2",
                cols: "40",
                maxlength: 1000
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'UrlSlug',
            width: 200,
            sortable: false,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 200
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'Published',
            index: 'Published',
            width: 100,
            align: 'center',
            editable: true,
            edittype: 'checkbox',
            editoptions: {
                value: "true:false",
                defaultValue: 'false'
            }
        });

        columns.push({
            name: 'PostedOn',
            index: 'PostedOn',
            width: 150,
            align: 'center',
            sorttype: 'date',
            datefmt: 'm/d/Y'
        });

        columns.push({
            name: 'Modified',
            index: 'Modified',
            width: 100,
            align: 'center',
            sorttype: 'date',
            datefmt: 'm/d/Y'
        });

        // create the grid
        $(gridName).jqGrid({
            // server url and other ajax stuff
            url: '/Admin/Posts',
            datatype: 'json',
            mtype: 'GET',
            height: 'auto',

            // columns
            colNames: colNames,
            colModel: columns,

            // pagination options
            toppager: true,
            pager: pagerName,
            rowNum: 10,
            rowList: [10, 20, 30],

            // row number column
            rownumbers: true,
            rownumWidth: 40,

            // default sorting
            sortname: 'PostedOn',
            sortorder: 'desc',

            // display the no. of records message
            viewrecords: true,

            jsonReader: { repeatitems: false },

            loadComplete: function (data) {
                for (var i = 0; i < data.rows.length; i++) {
                    var tagStr = "";
                    for (var j = 0; j < data.rows[i].Tags.length; j++) {
                        if (tagStr) tagStr += ", "
                        tagStr += data.rows[i].Tags[j].Name;
                    }
                    $(gridName).setRowData(data.rows[i].Id, { "Tags": tagStr });
                }
            }
        });

        
        var addOptions = {
            url: '/Admin/AddPost',
            addCaption: 'Add Post',
            processData: "Saving...",
            width: 900,
            closeAfterAdd: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose
        };

        $(gridName).navGrid(pagerName, { cloneToTop: true, search: false }, {}, addOptions, {});
    };

    $("#tabs").tabs({
        load: function (event, ui) {
            console.log("tabs load");
        },

        activate: function (event, ui) {
            console.log("tabs actuiivate");
        },

        create: function (event, ui) {
            console.log("tabs create");
            if (!ui.tab.isLoaded) {

                var gdMgr = JustBlog.GridManager,
                    fn, gridName, pagerName;
                console.log(ui.tab.index());
                switch (ui.tab.index()) {
                    case 0:
                        fn = gdMgr.postsGrid;
                        gridName = "#tablePosts";
                        pagerName = "#pagerPosts";
                        break;
                    case 1:
                        fn = gdMgr.categoriesGrid;
                        gridName = "#tableCats";
                        pagerName = "#pagerCats";
                        break;
                    case 2:
                        fn = gdMgr.tagsGrid;
                        gridName = "#tableTags";
                        pagerName = "#pagerTags";
                        break;
                };
                fn(gridName, pagerName);
                ui.tab.isLoaded = true;
            }
        }
    });
});