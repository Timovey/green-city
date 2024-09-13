export const taxerMenu = [
  {
    text: "Транспортные средства",
    ico: "mdi-truck",
    sublist: [
      {
        text: "Транспортные средства",
        ico: "mdi-car-estate",
        to: "/tax/transports",
      },
      {
        text: "Добавление",
        ico: "mdi-car-2-plus",
        to: "/tax/transports/edit/new",
      },
      {
        text: "График работы ТС",
        ico: "mdi-calendar-range-outline",
        to: "/tax/transports/shedule",
      },
      {
        text: "Ремонт и ТО",
        ico: "mdi-car-wrench",
        to: "/tax/transports/repair",
      },
    ],
  },
  {
    text: "Водители",
    ico: "mdi-steering",
    sublist: [
      {
        text: "Реестр водителей",
        ico: "mdi-account-group",
        to: "/tax/drivers",
      },
      {
        text: "Добавление",
        ico: "mdi-account-multiple-plus",
        to: "/tax/drivers/edit/new",
      },
      {
        text: "Отпуска/Больничные",
        ico: "mdi-calendar-month",
        to: "/tax/drivers/vacation",
      },
      {
        text: "Привязки",
        ico: "mdi-card-account-details",
        to: "/tax/drivers/fixing",
      },
    ],
  },
  {
    text: "План рабочих смен",
    ico: "mdi-calendar-week",
    to: "/tax/workshifts",
  },
  {
    text: "Экипажи",
    ico: "mdi-account-multiple-outline",
    to: "/tax/team",
  },
  {
    text: "Дежурные смены",
    ico: "mdi-account-hard-hat",
    to: "/tax/dutyshifts",
  },
  {
    text: "Аренда",
    ico: "mdi-car-key",
    to: "/tax/rent",
  },
];

export const aoMenu = [
  {
    text: "Контрагенты",
    ico: "mdi-account",
    to: "/ao/",
  },
  {
    text: "Ёмкости",
    ico: "mdi-apps",
    to: "/ao/capacity",
  },
  {
    text: "Контейнерные площадки",
    ico: "mdi-truck-cargo-container",
    to: "/ao/squares",
  },
  {
    text: "Договоры",
    ico: "mdi-file-document",
    to: "/ao/contracts",
  },
  {
    text: "Заявки",
    ico: "mdi-alert-box-outline",
    sublist: [
      {
        text: "Заявки по контрактам",
        ico: "mdi-file-document-edit-outline",
        to: "/ao/requests/contract",
      },
      {
        text: "Добавление по контрактам",
        ico: "mdi-pen-plus",
        to: "/ao/requests/contract/edit/new",
      },
      {
        text: "Заявки по картам",
        ico: "mdi-map",
        to: "/ao/requests/map",
      },
      {
        text: "Добавление по картам",
        ico: "mdi-map-plus",
        to: "/ao/requests/map/edit/new",
      },
    ],
  },
];

export const logistMenu = [
  {
    text: "Базовые маршруты",
    ico: "mdi-road",
    sublist: [
      {
        text: "Реестр БМ",
        ico: "mdi-road",
        to: "/logist/bm",
      },
      {
        text: "Добавление",
        ico: "mdi-truck-plus",
        to: "/logist/bm/edit/new",
      },
    ],
  },
  {
    text: "Рейсы",
    ico: "mdi-dump-truck",
    sublist: [
      {
        text: "Реестр Рейсов",
        ico: "mdi-dump-truck",
        to: "/logist/route",
      },
      {
        text: "Добавление",
        ico: "mdi-loupe",
        to: "/logist/route/edit/new",
      },
    ],
  },
  {
    text: "Заявки",
    ico: "mdi-alert-box-outline",
    sublist: [
      {
        text: "Заявки по контрактам",
        ico: "mdi-file-document-edit-outline",
        to: "/logist/requests/contract",
      },
      {
        text: "Добавление по контрактам",
        ico: "mdi-pen-plus",
        to: "/logist/requests/contract/edit/new",
      },
      {
        text: "Заявки по картам",
        ico: "mdi-map",
        to: "/logist/requests/map",
      },
      {
        text: "Добавление по картам",
        ico: "mdi-map-plus",
        to: "/logist/requests/map/edit/new",
      },
    ],
  },
  {
    text: "Перегенерация миссий",
    ico: "mdi-update",
    to: "/logist/regenerate",
  },
  {
    text: "Миссии",
    ico: "mdi-calendar-check-outline",
    to: "/logist/mission",
  },
  {
    text: "Движение транспортных средств",
    ico: "mdi-chart-timeline-variant",
    to: "/logist/doneTaskList",
  },
];

export const dispatcherMenu = [
  {
    text: "Подсобные рабочие",
    ico: "mdi-account-hard-hat",
    sublist: [
      {
        text: "Реестр рабочих",
        ico: "mdi-account-group",
        to: "/dispatcher/auxiliaryWorker",
      },
      {
        text: "Добавление",
        ico: "mdi-account-multiple-plus",
        to: "/dispatcher/auxiliaryWorker/edit/new",
      },
    ],
  },
  {
    text: "Рейсы",
    ico: "mdi-dump-truck",
    to: "/dispatcher/route",
  },
  {
    text: "Сменные задания",
    ico: "mdi-list-status",
    sublist: [
      {
        text: "Реестр СЗ",
        ico: "mdi-file-document-multiple",
        to: "/dispatcher/shiftTask",
      },
      {
        text: "Добавление",
        ico: "mdi-file-marker",
        to: "/dispatcher/shiftTask/create",
      },
    ],
  },
  {
    text: "Путевые листы",
    ico: "mdi-map-marker-path",
    sublist: [
      {
        text: "Реестр ПЛ",
        ico: "mdi-stack-overflow",
        to: "/dispatcher/travelSheet",
      },
      {
        text: "Добавление",
        ico: "mdi-map-plus",
        to: "/dispatcher/travelSheet/edit/new",
      },
    ],
  },
  {
    text: "Бригады",
    ico: "mdi-account-group-outline",
    sublist: [
      {
        text: "Реестр бригад",
        ico: "mdi-human-capacity-decrease",
        to: "/dispatcher/brigade",
      },
      {
        text: "Добавление",
        ico: "mdi-account-switch",
        to: "/dispatcher/brigade/create",
      },
    ],
  },
];
