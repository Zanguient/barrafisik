'use strict';

/**
 * Config constant
 */
app.constant('APP_MEDIAQUERY', {
    'desktopXL': 1200,
    'desktop': 992,
    'tablet': 768,
    'mobile': 480
});
app.constant('apiUrl', 'http://localhost:51246/');
app.constant('JS_REQUIRES', {
    //*** Scripts
    scripts: {
        //*** Javascript Plugins
        'modernizr': ['bower_components/components-modernizr/modernizr.js'],
        'moment': ['bower_components/moment/min/moment.min.js'],
        'spin': 'bower_components/spin.js/spin.js',

        //*** jQuery Plugins
        'perfect-scrollbar-plugin': ['bower_components/perfect-scrollbar/js/min/perfect-scrollbar.jquery.min.js', 'bower_components/perfect-scrollbar/css/perfect-scrollbar.min.css'],
        'ladda': ['bower_components/ladda/dist/ladda.min.js', 'bower_components/ladda/dist/ladda-themeless.min.css'],
        //'sweet-alert': ['bower_components/sweetalert/lib/sweet-alert.min.js', 'bower_components/sweetalert/lib/sweet-alert.css'],
        'sweet-alert': ['bower_components/sweetalert2/dist/sweetalert2.min.js', 'bower_components/sweetalert2/dist/sweetalert2.css'],
        'chartjs': 'bower_components/chartjs/Chart.min.js',
        'jquery-sparkline': 'bower_components/jquery.sparkline.build/dist/jquery.sparkline.min.js',
        'ckeditor-plugin': 'bower_components/ckeditor/ckeditor.js',
        'jquery-nestable-plugin': ['bower_components/jquery-nestable/jquery.nestable.js'],
        'touchspin-plugin': ['bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js', 'bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css'],


        //*** Controllers APP
        'clienteCtrl': 'app/controllers/cliente/clienteCtrl.js',
        'clienteCreateCtrl' : 'app/controllers/cliente/clienteCreateCtrl.js',
        'clienteEditCtrl': 'app/controllers/cliente/clienteEditCtrl.js',
        'clientePerfilCtrl': 'app/controllers/cliente/clientePerfilCtrl.js',
        'clienteAniversariantesCtrl': 'app/controllers/cliente/clienteAniversariantesCtrl.js',
        'horarioCtrl': 'app/controllers/horario/horarioCtrl.js',
        'filaEsperaCtrl': 'app/controllers/filaespera/filaEsperaCtrl.js',
        'dashboardCtrl': 'app/controllers/Dashboard/dashboardCtrl.js',
        'loginCtrl': 'app/controllers/login/loginCtrl.js',
        'accountCtrl': 'app/controllers/accounts/accountCtrl.js',
        'valoresCtrl': 'app/controllers/valores/valoresCtrl.js',
        'categoriaFinanceiraCtrl': 'app/controllers/categoriaFinanceira/categoriaFinanceiraCtrl.js',
        'receitasCtrl': 'app/controllers/receitas/receitasCtrl.js',
        'despesasCtrl': 'app/controllers/despesas/despesasCtrl.js',
        'relatorioFinanceiroCtrl': 'app/controllers/relatorioFinanceiro/relatorioFinanceiroCtrl.js',
        'relatorioFinanceiroReceitasCtrl': 'app/controllers/relatorioFinanceiro/relatorioFinanceiroReceitasCtrl.js',
        'relatorioFinanceiroDespesasCtrl': 'app/controllers/relatorioFinanceiro/relatorioFinanceiroDespesasCtrl.js',
        'tipoPagamentoCtrl': 'app/controllers/tipoPagamento/tipoPagamentoCtrl.js',
        'armazemCtrl': 'app/controllers/armazem/armazemCtrl.js',
        'funcionariosCtrl': 'app/controllers/funcionarios/funcionariosCtrl.js',
        'fornecedoresCtrl': 'app/controllers/fornecedores/fornecedoresCtrl.js',
        'produtosCategoriaCtrl': 'app/controllers/produtosCategoria/produtosCategoriaCtrl.js',
        'produtosCtrl': 'app/controllers/produtos/produtosCtrl.js',
        'movimentacaoCtrl': 'app/controllers/movimentacaoEstoque/movimentacaoCtrl.js',
        'estoqueCtrl': 'app/controllers/estoque/estoqueCtrl.js',
        'vendasCtrl': 'app/controllers/vendas/vendasCtrl.js',

        //*** Factories
        'clienteData' : 'app/factory/clienteData.js',
        'horarioData' : 'app/factory/horarioData.js',
        'filaEsperaData' : 'app/factory/filaEsperaData.js',
        'dashboardData' : 'app/factory/dashboardData.js',
        'accountsData' : 'app/factory/accountsData.js',
        'valoresData' : 'app/factory/valoresData.js',
        'receitasAvaliacaoFisicaData' : 'app/factory/receitasAvaliacaoFisicaData.js',
        'categoriaFinanceiraData': 'app/factory/categoriaFinanceiraData.js',
        'subCategoriaData': 'app/factory/subCategoriaData.js',
        'receitasData': 'app/factory/receitasData.js',
        'despesasData': 'app/factory/despesasData.js',
        'relatorioFinanceiroData': 'app/factory/relatorioFinanceiroData.js',
        'tipoPagamentoData': 'app/factory/tipoPagamentoData.js',
        'armazemData': 'app/factory/armazemData.js',
        'funcionariosData': 'app/factory/funcionariosData.js',
        'fornecedoresData': 'app/factory/fornecedoresData.js',
        'produtosCategoriaData': 'app/factory/produtosCategoriaData.js',
        'produtosData': 'app/factory/produtosData.js',
        'movimentacaoData': 'app/factory/movimentacaoData.js',
        'estoqueData': 'app/factory/estoqueData.js',
        'vendasData': 'app/factory/vendasData.js',
        'vendasProdutosData': 'app/factory/vendasProdutosData.js',

        //*** Controllers
        //'dashboardCtrl': 'assets/js/controllers/dashboardCtrl.js',
        'iconsCtrl': 'assets/js/controllers/iconsCtrl.js',
        'vAccordionCtrl': 'assets/js/controllers/vAccordionCtrl.js',
        'ckeditorCtrl': 'assets/js/controllers/ckeditorCtrl.js',
        'laddaCtrl': 'assets/js/controllers/laddaCtrl.js',
        'ngTableCtrl': 'assets/js/controllers/ngTableCtrl.js',
        'cropCtrl': 'assets/js/controllers/cropCtrl.js',
        'asideCtrl': 'assets/js/controllers/asideCtrl.js',
        'toasterCtrl': 'assets/js/controllers/toasterCtrl.js',
        'sweetAlertCtrl': 'assets/js/controllers/sweetAlertCtrl.js',
        'mapsCtrl': 'assets/js/controllers/mapsCtrl.js',
        'chartsCtrl': 'assets/js/controllers/chartsCtrl.js',
        'calendarCtrl': 'assets/js/controllers/calendarCtrl.js',
        'nestableCtrl': 'assets/js/controllers/nestableCtrl.js',
        'validationCtrl': ['assets/js/controllers/validationCtrl.js'],
        'userCtrl': ['assets/js/controllers/userCtrl.js'],
        'selectCtrl': 'assets/js/controllers/selectCtrl.js',
        'wizardCtrl': 'assets/js/controllers/wizardCtrl.js',
        'uploadCtrl': 'assets/js/controllers/uploadCtrl.js',
        'treeCtrl': 'assets/js/controllers/treeCtrl.js',
        'inboxCtrl': 'assets/js/controllers/inboxCtrl.js',
        'xeditableCtrl': 'assets/js/controllers/xeditableCtrl.js',
        'chatCtrl': 'assets/js/controllers/chatCtrl.js',
        
        //*** Filters
        'htmlToPlaintext': 'assets/js/filters/htmlToPlaintext.js',
        'clienteFilters': 'app/filters/clienteFilters.js'
    },
    //*** angularJS Modules
    modules: [{
        name: 'angularMoment',
        files: ['bower_components/angular-moment/angular-moment.min.js']
    }, {
        name: 'toaster',
        files: ['bower_components/AngularJS-Toaster/toaster.js', 'bower_components/AngularJS-Toaster/toaster.css']
    }, {
        name: 'angularBootstrapNavTree',
        files: ['bower_components/angular-bootstrap-nav-tree/dist/abn_tree_directive.js', 'bower_components/angular-bootstrap-nav-tree/dist/abn_tree.css']
    }, {
        name: 'angular-ladda',
        files: ['bower_components/angular-ladda/dist/angular-ladda.min.js']
    }, {
        name: 'ngTable',
        files: ['bower_components/ng-table/dist/ng-table.min.js', 'bower_components/ng-table/dist/ng-table.min.css']
    }, {
        name: 'ui.select',
        files: ['bower_components/angular-ui-select/dist/select.min.js', 'bower_components/angular-ui-select/dist/select.min.css', 'bower_components/select2/dist/css/select2.min.css', 'bower_components/select2-bootstrap-css/select2-bootstrap.min.css', 'bower_components/selectize/dist/css/selectize.bootstrap3.css']
    }, {
        name: 'ui.mask',
        files: ['bower_components/angular-ui-utils/mask.min.js']
    }, {
        name: 'ngImgCrop',
        files: ['bower_components/ngImgCrop/compile/minified/ng-img-crop.js', 'bower_components/ngImgCrop/compile/minified/ng-img-crop.css']
    }, {
        name: 'angularFileUpload',
        files: ['bower_components/angular-file-upload/angular-file-upload.min.js']
    }, {
        name: 'ngAside',
        files: ['bower_components/angular-aside/dist/js/angular-aside.min.js', 'bower_components/angular-aside/dist/css/angular-aside.min.css']
    }, {
        name: 'truncate',
        files: ['bower_components/angular-truncate/src/truncate.js']
    }, {
        name: 'oitozero.ngSweetAlert',
        files: ['bower_components/angular-sweetalert-promised/SweetAlert.min.js']
    }, {
        name: 'monospaced.elastic',
        files: ['bower_components/angular-elastic/elastic.js']
    }, {
        name: 'ngMap',
        files: ['bower_components/ngmap/build/scripts/ng-map.min.js']
    }, {
        name: 'tc.chartjs',
        files: ['bower_components/tc-angular-chartjs/dist/tc-angular-chartjs.min.js']
    }, {
        name: 'flow',
        files: ['bower_components/ng-flow/dist/ng-flow-standalone.min.js']
    }, {
        name: 'uiSwitch',
        files: ['bower_components/angular-ui-switch/angular-ui-switch.min.js', 'bower_components/angular-ui-switch/angular-ui-switch.min.css']
    }, {
        name: 'ckeditor',
        files: ['bower_components/angular-ckeditor/angular-ckeditor.min.js']
    }, {
        name: 'mwl.calendar',
        files: ['bower_components/angular-bootstrap-calendar/dist/js/angular-bootstrap-calendar.js', 'bower_components/angular-bootstrap-calendar/dist/js/angular-bootstrap-calendar-tpls.js', 'bower_components/angular-bootstrap-calendar/dist/css/angular-bootstrap-calendar.min.css']
    }, {
        name: 'ng-nestable',
        files: ['bower_components/ng-nestable/src/angular-nestable.js']
    }, {
        name: 'vAccordion',
        files: ['bower_components/v-accordion/dist/v-accordion.min.js', 'bower_components/v-accordion/dist/v-accordion.min.css']
    }, {
        name: 'xeditable',
        files: ['bower_components/angular-xeditable/dist/js/xeditable.min.js', 'bower_components/angular-xeditable/dist/css/xeditable.css', 'assets/js/config/config-xeditable.js']
    }, {
        name: 'checklist-model',
        files: ['bower_components/checklist-model/checklist-model.js']
    }]
});
