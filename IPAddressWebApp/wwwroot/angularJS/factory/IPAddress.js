'use strict';
ipApp.factory('IPAddress', function ($http) {

    let _currentClient = null;
    let _currentClientIpNet = null;

    let getPOSTurl = function () {
        let args = _currentClientIpNet;
        if (args.id == undefined)
            args.id = -1;
        if (args.ipnet == undefined)
            args.ipNetId = -1;
        //delete args.ipAddress;
        let url = "/api/IPAddress?Id=" + args.id
            + "&ClientId=" + args.clientId
            + "&ipNetId=" + args.ipNetId
            + "&oktet1=" + args.oktet1
            + "&oktet2=" + args.oktet2
            + "&oktet3=" + args.oktet3
            + "&oktet4=" + args.oktet4
            + "&mask=" + args.mask
        return url;
    }

    let getVoidIpNet = function () {
        return {
            id: null,
            clientId: null,
            ipNetId: null,
            oktet1: null,
            oktet2: null,
            oktet3: null,
            oktet4: null,
            mask: null
        }
    }


    let getPUTurl = function () {
        let args = _currentClientIpNet;
        //delete args.ipAddress;
        let url = "/api/IPAddress?Id=" + args.id
            + "&ClientId=" + args.clientId
            + "&ipNetId=" + args.ipNetId
            + "&oktet1=" + args.oktet1
            + "&oktet2=" + args.oktet2
            + "&oktet3=" + args.oktet3
            + "&oktet4=" + args.oktet4
            + "&mask=" + args.mask
        return url;
    }

    return {

        //установить текущего выбранного клиента
        setCurrentClient: function (client_) {
            _currentClient = client_;
            _currentClientIpNet = getVoidIpNet();
            _currentClientIpNet.clientId = client_.id;
        },

        //установить текущую сущность "клиент-подсеть"
        setCurrentClientIpNet: function (clientIpNet_) {
            _currentClientIpNet = clientIpNet_;
        },

        //Получить ip-адреса клиента
        getByClientId: function () {
            if (_currentClient == null)
                return null;

            return $http({
                method: 'GET',
                url: "/api/IPAddress/" + _currentClient.id
                , headers: {
                    'Content-Type': 'application/json'
                }
                //,params: { 'clientId_': _currentClient.Id }
            });
        },

        getVoidIpNet: getVoidIpNet,
        //getVoidIpNet: function () {
        //    return {
        //        id: null,
        //        clientId: null,
        //        ipNetId: null,
        //        oktet1: null,
        //        oktet2: null,
        //        oktet3: null,
        //        oktet4: null,
        //        mask: null
        //    }
        //},
        //Добавить
        addIpNetToClient: function () {
            return $http({
                method: 'POST',
                //url: "/api/IPAddress/",
                url: getPOSTurl(),

                //, headers: {
                //    'Content-Type': 'application/json'
                //},
                //params: _currentClientIpNet.id
                //data: _currentClientIpNet.id
            });
        },
        //Изменить подсеть
        changeCurrentIPnet: function () {
            if (_currentClientIpNet == null)
                return null;
            let args = _currentClientIpNet;
            //delete args.ipAddress;

            return $http({
                method: 'PUT',
               // url: "/api/IPAddress/"
                url: getPUTurl()
                //, headers: {
                //    'Content-Type': 'application/json'
                //}
                ////, params: { 'ipNet_': args }
                //, data: { 'ipNet_': args }
            });
        },
        //Удалить
        deleteIpNetFromClient: function () {
            
            return $http({
                method: 'DELETE',
                //url: "/api/IPAddress/",
                url: "/api/IPAddress/" + _currentClientIpNet.id,
                
                //, headers: {
                //    'Content-Type': 'application/json'
                //},
                //params: _currentClientIpNet.id
                //data: _currentClientIpNet.id
            });
        },
       

    }
});