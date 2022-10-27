﻿insert into Post (userId,title,body,tag,creationDate)
values(1,N'ما هي الطرق الجيدة لمنع حقن SQL؟',N'هذا السؤال له بالفعل إجابات هنا:
كيف يمكنني إضافة المدخلات المقدمة من المستخدم إلى جملة SQL؟ (إجابتين)
مغلق منذ 6 سنوات.
لا بد لي من برمجة نظام إدارة تطبيق لشركة OJT الخاصة بي. سيتم عمل الواجهة الأمامية في C # والنهاية الخلفية في SQL.

الآن لم أقم مطلقًا بمشروع بهذا النطاق من قبل ؛ في المدرسة كان لدينا دروس أساسية فقط حول SQL. بطريقة ما فشل مدرسنا تمامًا في مناقشة حقن SQL ، وهو أمر لم أتواصل معه الآن إلا من خلال القراءة عنه على الشبكة.

على أي حال ، سؤالي هو: كيف تمنع حقن SQL في C #؟ أعتقد بشكل غامض أنه يمكن القيام بذلك عن طريق إخفاء حقول النص الخاصة بالتطبيق بشكل صحيح بحيث يقبل فقط الإدخال بتنسيق محدد. على سبيل المثال: يجب أن يكون مربع نص البريد الإلكتروني بالتنسيق "example@examplecompany.tld". هل هذا النهج سيكون كافيا؟ أم أن .NET لديها طرق محددة مسبقًا تتعامل مع أشياء مثل هذه؟ هل يمكنني تطبيق عامل تصفية على مربع نص بحيث يقبل فقط تنسيق عنوان البريد الإلكتروني أو مربع نص الاسم حتى لا يقبل أحرفًا خاصة؟',N'برمجة الحاسب الآلي','2022-09-17 15:45:21.000');
insert into PostTags(postId,tagId)
values(1,1);

insert into Answer(postId,userId,answerText,creationDate)
values(1,2,N'الطريقة الصحيحة لتجنب هجمات حقن SQL ، بغض النظر عن قاعدة البيانات التي تستخدمها ، هي فصل البيانات عن SQL ، بحيث تظل البيانات بيانات ولن يتم تفسيرها أبدًا على أنها أوامر بواسطة محلل SQL. من الممكن إنشاء جملة SQL بأجزاء بيانات منسقة بشكل صحيح ، ولكن إذا لم تفهم التفاصيل تمامًا ، فيجب عليك دائمًا استخدام العبارات المعدة والاستعلامات ذات المعلمات. هذه هي عبارات SQL التي يتم إرسالها وتحليلها بواسطة خادم قاعدة البيانات بشكل منفصل عن أي معلمات. بهذه الطريقة يستحيل على المهاجم حقن SQL ضارة.','2022-09-18 15:45:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(2,N'إلغاء تسلسل JSON إلى كائن ديناميكي C #؟',N'هل هناك طريقة لإلغاء تسلسل محتوى JSON إلى نوع ديناميكي C #؟ سيكون من الجيد تخطي إنشاء مجموعة من الفئات لاستخدام DataContractJsonSerializer.
',N'برمجة الحاسب الآلي','2022-09-14 15:45:21.000');
insert into PostTags(postId,tagId)
values(2,1);

insert into Answer(postId,userId,answerText,creationDate)
values(2,1,N'إذا كنت سعيدًا بالاعتماد على مجموعة System.Web.Helpers ، فيمكنك استخدام كلاس Json','2022-09-15 15:45:21.000');

insert into Answer(postId,userId,answerText,creationDate)
values(2,3,N'الأمر بسيط جدًا باستخدام Json.NET
أيضًا باستخدام Newtonsoft.Json.Linq','2022-09-17 15:45:21.000');



insert into Post (userId,title,body,tag,creationDate)
values(3,N'كيف يختلف Docker عن الآلة الافتراضية؟',N'استمر في إعادة قراءة وثائق Docker لمحاولة فهم الفرق بين Docker و VM الكامل. كيف يمكنه توفير نظام ملفات كامل ، وبيئة شبكات معزولة ، وما إلى ذلك دون أن يكون ثقيلًا؟

لماذا يعد نشر البرنامج على صورة Docker (إذا كان هذا هو المصطلح الصحيح) أسهل من مجرد النشر في بيئة إنتاج متسقة؟',N'شبكات الحاسب الآلي','2022-09-12 15:45:21.000');
insert into PostTags(postId,tagId)
values(3,2);

insert into Answer(postId,userId,answerText,creationDate)
values(3,4,N'على عكس الآلة الافتراضية ، لا تحتاج الحاوية إلى تمهيد نواة نظام التشغيل ، لذلك يمكن إنشاء الحاويات في أقل من ثانية.','2022-09-12 15:55:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(4,N'ما الفرق بين المنفذ والمقبس؟',N'كان هذا سؤالًا طرحه أحد مهندسي البرمجيات في مؤسستي. أنا مهتم بأوسع تعريف.',N'شبكات الحاسب الآلي','2022-09-05 15:45:21.000');
insert into PostTags(postId,tagId)
values(4,2);


insert into Answer(postId,userId,answerText,creationDate)
values(4,1,N'مقبس الإنترنت هو مزيج من عنوان IP وبروتوكول ورقم المنفذ المرتبط به والذي قد توفر الخدمة البيانات عليه.','2022-09-07 15:55:21.000');

insert into Answer(postId,userId,answerText,creationDate)
values(4,2,N'مأخذ = عنوان IP + منفذ (عنوان رقمي)
يحددون معًا نقطة نهاية لاتصال الشبكة على الجهاز.','2022-09-09 15:55:21.000');




insert into Post (userId,title,body,tag,creationDate)
values(5,N'هل من العدل إلقاء اللوم على المؤلفين في تكاليف الكتب الباهظة؟',N'فهل هناك أي حجج مضادة؟ يرجى الحذر من أن هذا السؤال يتعلق فقط بالمؤلفين الأساتذة والذين يكتبون الكتب المدرسية أيضًا.

هل المؤلفون يساعدون الطلاب حقًا قدر الإمكان؟ على سبيل المثال ، هل يتم تحرير الكتب المدرسية ومراجعتها كل عامين بدافع الجشع؟ قد تتطلب بعض الموضوعات المرتبطة بإصلاحات الحياة الواقعية (مثل القانون) مثل هذا التغيير ، ولكن ماذا عن الاقتصاد',N'التسويق و المبيعات','2022-09-02 15:45:21.000');
insert into PostTags(postId,tagId)
values(5,3);

insert into Answer(postId,userId,answerText,creationDate)
values(5,4,N'إجابة مختصرة: نعم ولا.

جزئيًا ، لا هم ليسوا مسؤولين ، لأن بعضها يتعلق بالتكاليف والأسواق. لن ترى أبدًا كتبًا مدرسية معروضة بنفس السعر لكل صفحة مثل الروايات الخياليه ، لأن تكلفة إنشاء الكتاب المدرسي وتنظيمه مختلفة تمامًا. لذلك من المتوقع أن يكون السعر الأساسي للكتب المدرسية أعلى بكثير.','2022-09-02 16:55:21.000');


insert into Answer(postId,userId,answerText,creationDate)
values(5,6,N'المؤلفون لديهم الكثير من الخيارات للناشر للعمل معه ، وبعض الناشرين أكثر افتراسًا في أسعارهم من غيرهم. الأهم من ذلك ، أن هناك عددًا من مطابع الجامعات والمجتمع التي يمكن أن تمنح الكتاب ترقية جيدة وكذلك سعرًا معقولاً. ضع في اعتبارك ، على سبيل المثال ، مطبعة معهد ماساتشوستس للتكنولوجيا ، وهذه الكتب المستخدمة على نطاق واسع: كتاب خوارزميات CLR (S) (70 دولارًا) وكتاب برمجة SICP (49 دولارًا) ومجانيًا أيضًا.','2022-09-02 18:35:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(6,N'ما هو الفرق بين التسويق والبيع ؟',N'ما الذي يجعلك تختار العمل في المبيعات؟',N'التسويق و المبيعات','2022-09-01 15:45:21.000');
insert into PostTags(postId,tagId)
values(6,3);


insert into Post (userId,title,body,tag,creationDate)
values(7,N'في اي جامعه او معهد يقدم تخصص إدارة المستشفيات ؟',N'في اي جامعه او معهد يقدم تخصص إدارة المستشفيات ؟ بحثت كثيرا ولم اجد شي',N'إدارة المستشفيات','2022-08-23 15:45:21.000');
insert into PostTags(postId,tagId)
values(7,5);

insert into Answer(postId,userId,answerText,creationDate)
values(7,1,N'معهد الاداره العامة https://www.ipa.edu.sa/ar/training/#/prep-courses/detail/155?version-code=5','2022-08-25 18:35:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(8,N'ماهي أهداف إدارة الموارد البشرية',N'ممكن احد يشرح لي اهداف التخصص واذا توظفت بالتخصص ايش اهدافي بالعمل',N'إدارة الموارد البشرية','2022-08-15 15:45:21.000');
insert into PostTags(postId,tagId)
values(8,9);

insert into Answer(postId,userId,answerText,creationDate)
values(8,7,N'هناك العديد من الأهداف والمهام التي تترتّب على مسؤول الموارد البشريّة، وتشمل كافّة الشؤون المتعلّقة بتوظيف الأشخاص، وكذلك التعويضات الخاصة بهم، إلى جانب تحديد المهام وتحديد آليات العمل، وكذلك استحقاقاتهم، علماً أنّ الهدف الرئيسيّ المراد تحقيقه من حقل إدارة الموارد البشريّة هو تعزيز الإنتاجيّة لدى الموظفين، وتحقيق الاستغلال الأمثل للموظّفين، بصورة تعود بالفائدة على المنظّمة، وبالتالي نجد أنّ الموارد البشريّة تسعى بصورة مستمرة لاستقطاب المواهب والخبرات، وتعمل على تنميتها وتطويرها، وتوظيف كافّة السبل للاحتفاظ بها، وتسخير مهارات العاملين في خدمة الأعمال التجاريّة.','2022-08-16 18:35:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(9,N'ماهي شهادات ودورات إدارة سلاسل الإمداد المعتمدة دولياً',N'وايش افضلها من ناحية الطلب؟',N'إدارة سلاسل الإمداد','2022-08-10 15:45:21.000');
insert into PostTags(postId,tagId)
values(9,4);


insert into Answer(postId,userId,answerText,creationDate)
values(9,4,N'CSCP
تُساعدك دورة محترف معتمد في سلسلة التوريد (CSCP) على إظهار معرفتك ومهاراتك التنظيمية للخروج بعمليات أكثر بساطة. توفر هذه الدورة المعرفة اللازمة لفهم إدارة التكامل والتنسيق بين نشاطات سلسلة التوريد من البداية إلى النهاية.','2022-08-10 18:35:21.000');

insert into Answer(postId,userId,answerText,creationDate)
values(9,6,N'CPIM
تُمكنك دورة معتمد في إدارة الإنتاج والمخزون (CPIM) من إتقان العمليات الداخلية للمؤسسة وفهم إدارة المواد والجدولة الرئيسية والتنبؤ وتخطيط الإنتاج وتطبيق هذه المفاهيم على سلسلة التوريد الموسعة.','2022-08-12 18:35:21.000');


insert into Post (userId,title,body,tag,creationDate)
values(10,N'كيف يمكنني الحصول على قيم query string في JavaScript؟',N'هل هناك طريقة بدون plugin لاسترداد قيم query string عبر jQuery (أو بدونها)؟

إذا كان الأمر كذلك ، فكيف؟ إذا لم يكن كذلك ، فهل هناك plugin  يمكنه القيام بذلك؟',N'برمجة الحاسب الآلي','2022-08-01 15:45:21.000');
insert into PostTags(postId,tagId)
values(10,1);