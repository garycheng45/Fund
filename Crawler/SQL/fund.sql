-- DROP TABLE public."User";

CREATE TABLE public.Users
(
  sn serial,
  account character varying(32) NOT NULL,
  email character varying(256) NOT NULL,
  CONSTRAINT "User_PK" PRIMARY KEY (sn),
  CONSTRAINT "User_Uq_Acc" UNIQUE (account)
);

-- DROP TABLE public."NotifyType";

CREATE TABLE public.NotifyType
(
  sn serial,
  type character varying(32) NOT NULL,
  CONSTRAINT "NotifyType_PK" PRIMARY KEY (sn),
  CONSTRAINT "NotifyType_Uq" UNIQUE (sn, type)
);

CREATE TABLE public.Change
(
  sn serial,
  type character varying(16) NOT NULL,
  CONSTRAINT "Change_PK" PRIMARY KEY (sn),
  CONSTRAINT "Change_Uq" UNIQUE (sn, type)
);

CREATE TABLE public.DataResource
(
  sn serial,
  name character varying(32) NOT NULL DEFAULT 'Undefine'::character varying,
  CONSTRAINT "DataResource_PK" PRIMARY KEY (sn),
  CONSTRAINT "DataResource_Uq" UNIQUE (name)
);

-- DROP TABLE public."NotifyData";

CREATE TABLE public.NotifyData
(
  sn serial,
  url character varying(2048) NOT NULL,
  notifyId serial,
  name character varying(256) NOT NULL DEFAULT 'Undefine'::character varying,
  resourceId serial,
  CONSTRAINT "NotifyData_PK" PRIMARY KEY (sn),
  CONSTRAINT "NotifyData_FK_DataResource" FOREIGN KEY (resourceId)
      REFERENCES public.DataResource (sn) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT "NotifyData_FK_NotifyType" FOREIGN KEY (notifyId)
      REFERENCES public.NotifyType (sn) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT "NotifyData_Uq" UNIQUE (url)
);

-- DROP TABLE public."UserNotify";

CREATE TABLE public.UserNotify
(
  sn serial,
  nickName character varying(256),
  value real,
  userId serial,
  notifyId serial,
  changeType serial,
  CONSTRAINT "UserNotify_PK" PRIMARY KEY (sn),
  CONSTRAINT "UserNotify_FK_User" FOREIGN KEY (userId)
      REFERENCES public.Users (sn) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT "UsertNotify_FK_NotifyData" FOREIGN KEY (notifyId)
      REFERENCES public.NotifyData (sn) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT "UsertNotify_FK_Change" FOREIGN KEY (changeType)
      REFERENCES public.NotifyData (sn) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT
);

INSERT INTO NotifyType(type) VALUES('基金');
INSERT INTO NotifyType(type) VALUES('匯率');

INSERT INTO DataResource(name) VALUES('台銀');
INSERT INTO DataResource(name) VALUES('基富通');
INSERT INTO DataResource(name) VALUES('鉅亨網');

INSERT INTO Change(type) VALUES('漲');
INSERT INTO Change(type) VALUES('跌');

-- testing data ---
INSERT INTO User(account, email) VALUES('tony', 'garycheng45@gmail.com');
INSERT INTO NotifyData(url, name, notifyId, resourceId) VALUES('http://fUqd.bot.com.tw/w/wb/wb02A.djhtm?A=FLZ01-A710', '富蘭克林黃金美元A', 1, 1);
INSERT INTO UserNotify(userId, notifyId, nickName, value, changeType) VALUES(
   (select sn from User where account = 'tony'), 
   (select sn from NotifyData limit 1), 
   '富坦黃金',
   14.3,
   2
   );
