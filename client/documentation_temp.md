Документация системы (временная версия)
Примечание:
Этот документ временный и описывает текущие компоненты системы и используемые решения (в т.ч. костыли), 
пока не реализована полноценная backend-часть.

1. Роль и аутентификация пользователя
Маршрут: src/app/router/AppRouter.tsx
В данный момент внутри хука useEffect моделируется определение роли и аутентификация пользователя. Пока это сделано вручную.
Как только backend часть, связанная с авторизацией, будет готова, логика аутентификации будет переделана.


2. Страницы в приложении
Маршрут: src/app/router/index.ts
Инструкция по добавлению новой страницы:
Создайте папку и компонент новой страницы в src/pages.
Создайте новый параметр в enum RouteNames (файл: src/app/router/index.ts).
Добавьте новый элемент в массив страниц:
В качестве параметра path используйте новый параметр из enum RouteNames.
В качестве параметра element укажите компонент страницы.


3. Разные варианты визуала шапки приложения
В приложении предусмотрено 3 версии шапки.
Состояние шапки (headerVersion) вынесено в стор: src/layouts/Header/store/header.ts.
При создании новой страницы необходимо указать, какой именно тип шапки (THeaderVersion) требуется на данной странице.
Пример настройки можно посмотреть в файле: src/pages/MainPage/MainPage.tsx.


4. Административная панель
Административная панель присутствует только на определённых страницах.
Страницы с отображением админской панели вынесены в массив ссылок: src/layouts/AppLayout/constants/adminPanelRoutes.ts.
Если требуется добавить административную панель на новую страницу, добавьте её URL в массив adminPanelRoutes.


5. Поиск в шапке
Поиск встроен в шапку, которая отображается на каждой странице.
Поиск используется на разных страницах для запроса данных по различным маршрутам.
Инструкция по реализации поиска:
Для задания URL для получения данных используйте:
    const requestUrl = useHeaderStore(store => store.requestUrl);
Ответ после нажатия кнопки поиска и выполнения запроса будет доступен в:
    const response = useHeaderStore(store => store.response);
Для отслеживания состояния загрузки запроса используйте:
    const requestIsLoading = useHeaderStore(store => store.requestIsLoading);
Это состояние можно использовать для отображения компонента Loader.

6. Кастыль с логинингом 
Что бы зайнти в аккаунт пока нужно просто ввести в поле почты user or admin и вы сможете войти в соответствующий аккаунт 