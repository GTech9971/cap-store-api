-- public.categories definition

-- Drop table

-- DROP TABLE public.categories;
-- public."__EFMigrationsHistory" definition

-- Drop table

-- DROP TABLE public."__EFMigrationsHistory";

CREATE TABLE public."__EFMigrationsHistory" (
	"MigrationId" varchar(150) NOT NULL,
	"ProductVersion" varchar(32) NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


CREATE TABLE public.categories (
	id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	"name" text NOT NULL,
	image_url text NULL,
	CONSTRAINT "PK_categories" PRIMARY KEY (id)
);
CREATE INDEX "IX_categories_name" ON public.categories USING btree (name);

COPY public.categories FROM '/tmp/init_data/categories.csv' DELIMITER ',' CSV HEADER;

-- public.makers definition

-- Drop table

-- DROP TABLE public.makers;

CREATE TABLE public.makers (
	id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	"name" text NOT NULL,
	image_url text NULL,
	CONSTRAINT "PK_makers" PRIMARY KEY (id)
);
CREATE INDEX "IX_makers_name" ON public.makers USING btree (name);

COPY public.makers FROM '/tmp/init_data/makers.csv' DELIMITER ',' CSV HEADER;


-- public.components definition

-- Drop table

-- DROP TABLE public.components;

CREATE TABLE public.components (
	component_id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	"name" text NOT NULL,
	model_name text NOT NULL,
	description text NOT NULL,
	category_id int4 NOT NULL,
	maker_id int4 NOT NULL,
	CONSTRAINT "PK_components" PRIMARY KEY (component_id)
);
CREATE INDEX "IX_components_category_id" ON public.components USING btree (category_id);
CREATE INDEX "IX_components_maker_id" ON public.components USING btree (maker_id);


-- public.components foreign keys

ALTER TABLE public.components ADD CONSTRAINT "FK_components_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
ALTER TABLE public.components ADD CONSTRAINT "FK_components_makers_maker_id" FOREIGN KEY (maker_id) REFERENCES public.makers(id) ON DELETE CASCADE;


-- public.component_images definition

-- Drop table

-- DROP TABLE public.component_images;

CREATE TABLE public.component_images (
	id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	component_id int4 NOT NULL,
	image_url text NOT NULL,
	"ComponentDataComponentId" int4 NULL,
	CONSTRAINT "PK_component_images" PRIMARY KEY (id)
);
CREATE INDEX "IX_component_images_ComponentDataComponentId" ON public.component_images USING btree ("ComponentDataComponentId");


-- public.component_images foreign keys

ALTER TABLE public.component_images ADD CONSTRAINT "FK_component_images_components_ComponentDataComponentId" FOREIGN KEY ("ComponentDataComponentId") REFERENCES public.components(component_id);