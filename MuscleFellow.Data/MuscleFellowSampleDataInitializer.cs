using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MuscleFellow.Models;
using MuscleFellow.Models.BasicInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MuscleFellow.Data
{
    public class MuscleFellowSampleDataInitializer
    {
        private MuscleFellowDbContext _dbContext = null;

        private readonly string _brandRootPath = "/Assets/Brands/";
        private readonly string _productRootPath = "/Assets/Products/";
        public MuscleFellowSampleDataInitializer(MuscleFellowDbContext context)
        {
            _dbContext = context;
        }

        public async Task LoadSampleDataAsync()
        {
            // Add Categories
            _dbContext.Categories.Add(new Category { CategoryName = "健身器械" });
            _dbContext.Categories.Add(new Category { CategoryName = "健身补剂" });

            // Add Brands
            _dbContext.Brands.Add(new Brand
            {
                BrandName = "LifeFitness",
                Logo = BrandImagePath("Life-Fitness.jpg"),
                Description = "悠久历史，一脉相承；卓越的产品和设计，设立行业标准。最先进的人体工程学研究，世界领先的生产制造流程，各种最严格测试。对健身的专注和执着，与众不同的健身感受，健身房布局与力健之旅。健身专家及合作伙伴，脱颖而出，全球第一。美国500强公司（康体娱乐设备排名第一），美国上市公司宾士域集团全资附属子公司，全球高品质商用和家用设备设计和制造行业的领导者。自1968年开始，以高效耐用、使用简单等优点树立了行业标准，市场领导地位无可撼动。全球设有10个直属办公室（美国 / 加拿大 / 拉美，亚太地区，巴西，欧洲 / 中东 / 非洲，德国 / 瑞士，澳大利亚，意大利，日本，西班牙，英国） 产品销至120个国家，全球范围内市场占有率第一 "
            });
            _dbContext.Brands.Add(new Brand
            {
                BrandName = "MASSFIT",
                Logo = BrandImagePath("massfit.jpg"),
                Description = "MASSFIT（马西）是一个专业力量训练器材的著名品牌，产品汇集欧美澳三地的杰出产品，领先国内市场，不断为国内健身爱好者提供专业的、先进的、高品质的健身器材，并受到多方好评！此次马西旗舰店登录京东商城，将为MASSFIT（马西）品牌的健身器材开辟更加广阔的国内市场。"
            });
            _dbContext.Brands.Add(new Brand
            {
                BrandName = "MET-Rx(美瑞克斯)",
                Logo = BrandImagePath("MET-Rx.png"),
                Description = "上世纪70年代，波斯顿大学神经生理学教授、医学博士Scott Connelly 在研究神经与肌肉能力学，神经与肌肉营养性相互关系时，发现不同的蛋白矩阵对肌肉构成有影响，这引起了他极大的兴趣，经过20多年潜心研究，Connelly博士开发出美瑞克斯(MET-Rx®)产品的核心蛋白配方,Metamyosyn专利配方蛋白，这种蛋白通过为人体提供不同生理效用及特定比例的优质蛋白，帮助人体构建肌肉并减少体脂含量，这种产品一经问世立即得到了世界顶级运动员、好莱坞明星和众多健身爱好者的积极响应和高度评价。此后美瑞克斯(MET-Rx®) 相继推出几个系列的蛋白、氨基酸、燃脂等产品，被应用于专业运动员及普通大众健身过程中的力量、体形和耐力的训练。"
            });
            _dbContext.Brands.Add(new Brand
            {
                BrandName = "MuscleTech(肌肉科技)",
                Logo = BrandImagePath("MuscleTech.jpg"),
                Description = "肌肉科技（Muscletech）全球运动补剂的神话。全球最知名品牌之一，肌肉科技明星团队包括了乔卡特，菲尔·希斯，德克斯特·杰克逊等超级明星。肌肉科技产品无处不在，在美国任何一本健身杂志，每一场健美比赛，所有营养品专卖店，肌肉科技的标志都会矗立眼前。肌肉科技的追随者以几何速度每天增长。肌肉科技超级明星团队代表全球健美的辉煌，肌肉科技的竞争对手们也只能每天努力奋斗着甘居亚军，这就是肌肉科技文化。"
            });
            _dbContext.Brands.Add(new Brand
            {
                BrandName = "PROIRON",
                Logo = BrandImagePath("PROIRON.jpg"),
                Description = "PROIRON是一个由全球最大的举重类健身器材制造商和供应商山西新和健身器材公司注册的健身器品牌，以稳定优良的品质，合理的价格和完善的服务在全球市场树立了卓越的声誉。"
            });

            // Submit data into Database
            await _dbContext.SaveChangesAsync();

            // Get CategoryIds
            string tester = string.Empty;
            tester = "健身器械";
            int equipmentId = _dbContext.Categories.SingleOrDefault(e => e.CategoryName == tester).CategoryID;
            tester = "健身补剂";
            int supplementId = _dbContext.Categories.SingleOrDefault(e => e.CategoryName == tester).CategoryID;

            // Get BrandIds
            tester = "LifeFitness";
            int lifefitnessId = _dbContext.Brands.SingleOrDefault(b => b.BrandName == tester).BrandID;
            tester = "MASSFIT";
            int massfitId = _dbContext.Brands.SingleOrDefault(b => b.BrandName == tester).BrandID;
            tester = "MET-Rx(美瑞克斯)";
            int metrxId = _dbContext.Brands.SingleOrDefault(b => b.BrandName == tester).BrandID;
            tester = "MuscleTech(肌肉科技)";
            int muscletechId = _dbContext.Brands.SingleOrDefault(b => b.BrandName == tester).BrandID;
            tester = "PROIRON";
            int proironId = _dbContext.Brands.SingleOrDefault(b => b.BrandName == tester).BrandID;

            // Add Products
            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                ProductName = "美国力健家用静音健身车",
                BrandID = lifefitnessId,
                CategoryID = equipmentId,
                Description = "人性化设计，走越式设计家辅助扶手方便用户上、下机。多种座椅调节方式确保不同提醒的人群可以体验到舒适、自然的训练。",
                ThumbnailImage = ProductThumbImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 23.40f,
                Height = 113.00f,
                UnitOfLength = "cm",
                Weight = 52.8f,
                UnitOfWeight = "kg",
                UnitPrice = 30660.00f,
                Currency = "RMB"
            });

            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("5A187D13-3023-4A8B-4448-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("5A187D13-3023-4A8B-4448-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();

            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                ProductName = "美国Life Fitness力健多功能综合训练小飞鸟家用肌肉力量健身器G7",
                BrandID = lifefitnessId,
                CategoryID = equipmentId,
                Description = "力健力量设备借鉴了Hammer豪迈力量系列的创新特色，豪迈品牌是高级健身俱乐部和专业运动员必选的力量训练设备。",
                ThumbnailImage = ProductThumbImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 89.40f,
                Height = 193.00f,
                UnitOfLength = "cm",
                Weight = 98.8f,
                UnitOfWeight = "kg",
                UnitPrice = 46170.00f,
                Currency = "RMB"
            });

            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9ED6C85F-F379-448B-4449-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9ED6C85F-F379-448B-4449-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();

            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                ProductName = "美国Life Fitness力健力量健身综合训练器材家用多功能室内设备G4",
                BrandID = lifefitnessId,
                CategoryID = equipmentId,
                Description = "为了强化对下半身肌肉群的训练，用户可以选配蹬腿机/小腿训练机进行训练。中滑轮设计方便用户完成腹部、手臂、肩部和胸部的动作训练。",
                ThumbnailImage = ProductThumbImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 89.40f,
                Height = 193.00f,
                UnitOfLength = "cm",
                Weight = 98.8f,
                UnitOfWeight = "kg",
                UnitPrice = 43170.00f,
                Currency = "RMB"
            });
            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("9FB5DDAE-41D2-4B62-444A-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("9FB5DDAE-41D2-4B62-444A-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();

            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                ProductName = "美国力健Life Fitness家用商用多功能跑步机T5",
                BrandID = lifefitnessId,
                CategoryID = equipmentId,
                Description = "超静音，液晶触摸屏幕。",
                ThumbnailImage = ProductThumbImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 89.40f,
                Height = 193.00f,
                UnitOfLength = "cm",
                Weight = 98.8f,
                UnitOfWeight = "kg",
                UnitPrice = 56000.00f,
                Currency = "RMB"
            });
            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("81DE0364-0401-4F38-444B-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("81DE0364-0401-4F38-444B-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();

            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                ProductName = "肌肉科技白金乳清蛋白粉5磅",
                BrandID = muscletechId,
                CategoryID = supplementId,
                Description = "乳清蛋白可以被身体快速吸收，易于消化，是训练后修复肌肉的优选。",
                ThumbnailImage = ProductThumbImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 89.40f,
                Height = 193.00f,
                UnitOfLength = "cm",
                Weight = 2485f,
                UnitOfWeight = "g",
                UnitPrice = 558.00f,
                Currency = "RMB"
            });
            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("42FE79E8-FA1D-42AE-444C-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("42FE79E8-FA1D-42AE-444C-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();


            _dbContext.Products.Add(new Product
            {
                ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                ProductName = "肌肉科技复合蛋白粉健身增健肌粉7磅",
                BrandID = muscletechId,
                CategoryID = supplementId,
                Description = "超纯度乳清分离蛋白及多肽，最先进的错流细微过滤生产工艺。通过冷冻技术，保留丰富的蛋白质生物活性剂吸收率。每份含5.4可支链氨基酸和11.6克必需氨基酸",
                ThumbnailImage = ProductThumbImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "1.jpg"),
                Length = 124.50f,
                Width = 89.40f,
                Height = 193.00f,
                UnitOfLength = "cm",
                Weight = 2485f,
                UnitOfWeight = "g",
                UnitPrice = 558.00f,
                Currency = "RMB"
            });
            _dbContext.ProductImages.AddRange(new ProductImage[]
            {
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "1.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "2.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "3.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "4.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "5.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "6.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "7.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "8.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "9.jpg")
                },
                new ProductImage
                {
                    ProductID = new Guid("178A79A4-F8DF-4FD9-444D-08D371929CF9"),
                    RelativeUrl = ProductDetailImagePath("178A79A4-F8DF-4FD9-444D-08D371929CF9", "10.jpg")
                }
            });
            await _dbContext.SaveChangesAsync();
        }
        public async Task LoadBasicInformationAsync()
        {
            // Add provinces
            _dbContext.Provinces.AddRange(
                new Province[] {
                    new Province {Name = "北京市" },
                    new Province {Name = "天津市" },
                    new Province {Name = "上海市" },
                    new Province {Name = "重庆市" },
                    new Province {Name = "河北省" },
                    new Province {Name = "山西省" },
                    new Province {Name = "台湾省" },
                    new Province {Name = "辽宁省" },
                    new Province {Name = "吉林省" },
                    new Province {Name = "黑龙江省" },
                    new Province {Name = "江苏省" },
                    new Province {Name = "浙江省" },
                    new Province {Name = "安徽省" },
                    new Province {Name = "福建省" },
                    new Province {Name = "江西省" },
                    new Province {Name = "山东省" },
                    new Province {Name = "河南省" },
                    new Province {Name = "湖北省" },
                    new Province {Name = "湖南省" },
                    new Province {Name = "广东省" },
                    new Province {Name = "甘肃省" },
                    new Province {Name = "四川省" },
                    new Province {Name = "贵州省" },
                    new Province {Name = "海南省" },
                    new Province {Name = "云南省" },
                    new Province {Name = "青海省" },
                    new Province {Name = "陕西省" },
                    new Province {Name = "广西壮族自治区" },
                    new Province {Name = "西藏自治区" },
                    new Province {Name = "宁夏回族自治区" },
                    new Province {Name = "新疆维吾尔自治区" },
                    new Province {Name = "内蒙古自治区" },
                    new Province {Name = "澳门特别行政区" },
                    new Province {Name = "香港特别行政区" },
                });
            await _dbContext.SaveChangesAsync();

            // add cities
            _dbContext.Cities.AddRange(
                new City[] {
                    new City { CityIndex = 1, ProvinceID = 1, Name = "北京市"},
                    new City { CityIndex = 1, ProvinceID = 2, Name = "天津市"},
                    new City { CityIndex = 1, ProvinceID = 3, Name = "上海市"},
                    new City { CityIndex = 1, ProvinceID = 4, Name = "重庆市"},
                    new City { CityIndex = 1, ProvinceID = 5, Name = "石家庄市"},
                    new City { CityIndex = 2, ProvinceID = 5, Name = "唐山市"},
                    new City { CityIndex = 3, ProvinceID = 5, Name = "秦皇岛市"},
                    new City { CityIndex = 4, ProvinceID = 5, Name = "邯郸市"},
                    new City { CityIndex = 5, ProvinceID = 5, Name = "邢台市"},
                    new City { CityIndex = 6, ProvinceID = 5, Name = "保定市"},
                    new City { CityIndex = 7, ProvinceID = 5, Name = "张家口市"},
                    new City { CityIndex = 8, ProvinceID = 5, Name = "承德市"},
                    new City { CityIndex = 9, ProvinceID = 5, Name = "沧州市"},
                    new City { CityIndex = 10, ProvinceID = 5, Name = "廊坊市"},
                    new City { CityIndex = 11, ProvinceID = 5, Name = "衡水市"},
                    new City { CityIndex = 1, ProvinceID = 6, Name = "太原市"},
                    new City { CityIndex = 2, ProvinceID = 6, Name = "大同市"},
                    new City { CityIndex = 3, ProvinceID = 6, Name = "阳泉市"},
                    new City { CityIndex = 4, ProvinceID = 6, Name = "长治市"},
                    new City { CityIndex = 5, ProvinceID = 6, Name = "晋城市"},
                    new City { CityIndex = 6, ProvinceID = 6, Name = "朔州市"},
                    new City { CityIndex = 7, ProvinceID = 6, Name = "晋中市"},
                    new City { CityIndex = 8, ProvinceID = 6, Name = "运城市"},
                    new City { CityIndex = 9, ProvinceID = 6, Name = "忻州市"},
                    new City { CityIndex = 10, ProvinceID = 6, Name = "临汾市"},
                    new City { CityIndex = 11, ProvinceID = 6, Name = "吕梁市"},
                    new City { CityIndex = 1, ProvinceID = 7, Name = "台北市"},
                    new City { CityIndex = 2, ProvinceID = 7, Name = "高雄市"},
                    new City { CityIndex = 3, ProvinceID = 7, Name = "基隆市"},
                    new City { CityIndex = 4, ProvinceID = 7, Name = "台中市"},
                    new City { CityIndex = 5, ProvinceID = 7, Name = "台南市"},
                    new City { CityIndex = 6, ProvinceID = 7, Name = "新竹市"},
                    new City { CityIndex = 7, ProvinceID = 7, Name = "嘉义市"},
                    new City { CityIndex = 8, ProvinceID = 7, Name = "台北县"},
                    new City { CityIndex = 9, ProvinceID = 7, Name = "宜兰县"},
                    new City { CityIndex = 10, ProvinceID = 7, Name = "桃园县"},
                    new City { CityIndex = 11, ProvinceID = 7, Name = "新竹县"},
                    new City { CityIndex = 12, ProvinceID = 7, Name = "苗栗县"},
                    new City { CityIndex = 13, ProvinceID = 7, Name = "台中县"},
                    new City { CityIndex = 14, ProvinceID = 7, Name = "彰化县"},
                    new City { CityIndex = 15, ProvinceID = 7, Name = "南投县"},
                    new City { CityIndex = 16, ProvinceID = 7, Name = "云林县"},
                    new City { CityIndex = 17, ProvinceID = 7, Name = "嘉义县"},
                    new City { CityIndex = 18, ProvinceID = 7, Name = "台南县"},
                    new City { CityIndex = 19, ProvinceID = 7, Name = "高雄县"},
                    new City { CityIndex = 20, ProvinceID = 7, Name = "屏东县"},
                    new City { CityIndex = 21, ProvinceID = 7, Name = "澎湖县"},
                    new City { CityIndex = 22, ProvinceID = 7, Name = "台东县"},
                    new City { CityIndex = 23, ProvinceID = 7, Name = "花莲县"},
                    new City { CityIndex = 1, ProvinceID = 8, Name = "沈阳市"},
                    new City { CityIndex = 2, ProvinceID = 8, Name = "大连市"},
                    new City { CityIndex = 3, ProvinceID = 8, Name = "鞍山市"},
                    new City { CityIndex = 4, ProvinceID = 8, Name = "抚顺市"},
                    new City { CityIndex = 5, ProvinceID = 8, Name = "本溪市"},
                    new City { CityIndex = 6, ProvinceID = 8, Name = "丹东市"},
                    new City { CityIndex = 7, ProvinceID = 8, Name = "锦州市"},
                    new City { CityIndex = 8, ProvinceID = 8, Name = "营口市"},
                    new City { CityIndex = 9, ProvinceID = 8, Name = "阜新市"},
                    new City { CityIndex = 10, ProvinceID = 8, Name = "辽阳市"},
                    new City { CityIndex = 11, ProvinceID = 8, Name = "盘锦市"},
                    new City { CityIndex = 12, ProvinceID = 8, Name = "铁岭市"},
                    new City { CityIndex = 13, ProvinceID = 8, Name = "朝阳市"},
                    new City { CityIndex = 14, ProvinceID = 8, Name = "葫芦岛市"},
                    new City { CityIndex = 1, ProvinceID = 9, Name = "长春市"},
                    new City { CityIndex = 2, ProvinceID = 9, Name = "吉林市"},
                    new City { CityIndex = 3, ProvinceID = 9, Name = "四平市"},
                    new City { CityIndex = 4, ProvinceID = 9, Name = "辽源市"},
                    new City { CityIndex = 5, ProvinceID = 9, Name = "通化市"},
                    new City { CityIndex = 6, ProvinceID = 9, Name = "白山市"},
                    new City { CityIndex = 7, ProvinceID = 9, Name = "松原市"},
                    new City { CityIndex = 8, ProvinceID = 9, Name = "白城市"},
                    new City { CityIndex = 9, ProvinceID = 9, Name = "延边朝鲜族自治州"},
                    new City { CityIndex = 1, ProvinceID = 10, Name = "哈尔滨市"},
                    new City { CityIndex = 2, ProvinceID = 10, Name = "齐齐哈尔市"},
                    new City { CityIndex = 3, ProvinceID = 10, Name = "鹤岗市"},
                    new City { CityIndex = 4, ProvinceID = 10, Name = "双鸭山市"},
                    new City { CityIndex = 5, ProvinceID = 10, Name = "鸡西市"},
                    new City { CityIndex = 6, ProvinceID = 10, Name = "大庆市"},
                    new City { CityIndex = 7, ProvinceID = 10, Name = "伊春市"},
                    new City { CityIndex = 8, ProvinceID = 10, Name = "牡丹江市"},
                    new City { CityIndex = 9, ProvinceID = 10, Name = "佳木斯市"},
                    new City { CityIndex = 10, ProvinceID = 10, Name = "七台河市"},
                    new City { CityIndex = 11, ProvinceID = 10, Name = "黑河市"},
                    new City { CityIndex = 12, ProvinceID = 10, Name = "绥化市"},
                    new City { CityIndex = 13, ProvinceID = 10, Name = "大兴安岭地区"},
                    new City { CityIndex = 1, ProvinceID = 11, Name = "南京市"},
                    new City { CityIndex = 2, ProvinceID = 11, Name = "无锡市"},
                    new City { CityIndex = 3, ProvinceID = 11, Name = "徐州市"},
                    new City { CityIndex = 4, ProvinceID = 11, Name = "常州市"},
                    new City { CityIndex = 5, ProvinceID = 11, Name = "苏州市"},
                    new City { CityIndex = 6, ProvinceID = 11, Name = "南通市"},
                    new City { CityIndex = 7, ProvinceID = 11, Name = "连云港市"},
                    new City { CityIndex = 8, ProvinceID = 11, Name = "淮安市"},
                    new City { CityIndex = 9, ProvinceID = 11, Name = "盐城市"},
                    new City { CityIndex = 10, ProvinceID = 11, Name = "扬州市"},
                    new City { CityIndex = 11, ProvinceID = 11, Name = "镇江市"},
                    new City { CityIndex = 12, ProvinceID = 11, Name = "泰州市"},
                    new City { CityIndex = 13, ProvinceID = 11, Name = "宿迁市"},
                    new City { CityIndex = 1, ProvinceID = 12, Name = "杭州市"},
                    new City { CityIndex = 2, ProvinceID = 12, Name = "宁波市"},
                    new City { CityIndex = 3, ProvinceID = 12, Name = "温州市"},
                    new City { CityIndex = 4, ProvinceID = 12, Name = "嘉兴市"},
                    new City { CityIndex = 5, ProvinceID = 12, Name = "湖州市"},
                    new City { CityIndex = 6, ProvinceID = 12, Name = "绍兴市"},
                    new City { CityIndex = 7, ProvinceID = 12, Name = "金华市"},
                    new City { CityIndex = 8, ProvinceID = 12, Name = "衢州市"},
                    new City { CityIndex = 9, ProvinceID = 12, Name = "舟山市"},
                    new City { CityIndex = 10, ProvinceID = 12, Name = "台州市"},
                    new City { CityIndex = 11, ProvinceID = 12, Name = "丽水市"},
                    new City { CityIndex = 1, ProvinceID = 13, Name = "合肥市"},
                    new City { CityIndex = 2, ProvinceID = 13, Name = "芜湖市"},
                    new City { CityIndex = 3, ProvinceID = 13, Name = "蚌埠市"},
                    new City { CityIndex = 4, ProvinceID = 13, Name = "淮南市"},
                    new City { CityIndex = 5, ProvinceID = 13, Name = "马鞍山市"},
                    new City { CityIndex = 6, ProvinceID = 13, Name = "淮北市"},
                    new City { CityIndex = 7, ProvinceID = 13, Name = "铜陵市"},
                    new City { CityIndex = 8, ProvinceID = 13, Name = "安庆市"},
                    new City { CityIndex = 9, ProvinceID = 13, Name = "黄山市"},
                    new City { CityIndex = 10, ProvinceID = 13, Name = "滁州市"},
                    new City { CityIndex = 11, ProvinceID = 13, Name = "阜阳市"},
                    new City { CityIndex = 12, ProvinceID = 13, Name = "宿州市"},
                    new City { CityIndex = 13, ProvinceID = 13, Name = "巢湖市"},
                    new City { CityIndex = 14, ProvinceID = 13, Name = "六安市"},
                    new City { CityIndex = 15, ProvinceID = 13, Name = "亳州市"},
                    new City { CityIndex = 16, ProvinceID = 13, Name = "池州市"},
                    new City { CityIndex = 17, ProvinceID = 13, Name = "宣城市"},
                    new City { CityIndex = 1, ProvinceID = 14, Name = "福州市"},
                    new City { CityIndex = 2, ProvinceID = 14, Name = "厦门市"},
                    new City { CityIndex = 3, ProvinceID = 14, Name = "莆田市"},
                    new City { CityIndex = 4, ProvinceID = 14, Name = "三明市"},
                    new City { CityIndex = 5, ProvinceID = 14, Name = "泉州市"},
                    new City { CityIndex = 6, ProvinceID = 14, Name = "漳州市"},
                    new City { CityIndex = 7, ProvinceID = 14, Name = "南平市"},
                    new City { CityIndex = 8, ProvinceID = 14, Name = "龙岩市"},
                    new City { CityIndex = 9, ProvinceID = 14, Name = "宁德市"},
                    new City { CityIndex = 1, ProvinceID = 15, Name = "南昌市"},
                    new City { CityIndex = 2, ProvinceID = 15, Name = "景德镇市"},
                    new City { CityIndex = 3, ProvinceID = 15, Name = "萍乡市"},
                    new City { CityIndex = 4, ProvinceID = 15, Name = "九江市"},
                    new City { CityIndex = 5, ProvinceID = 15, Name = "新余市"},
                    new City { CityIndex = 6, ProvinceID = 15, Name = "鹰潭市"},
                    new City { CityIndex = 7, ProvinceID = 15, Name = "赣州市"},
                    new City { CityIndex = 8, ProvinceID = 15, Name = "吉安市"},
                    new City { CityIndex = 9, ProvinceID = 15, Name = "宜春市"},
                    new City { CityIndex = 10, ProvinceID = 15, Name = "抚州市"},
                    new City { CityIndex = 11, ProvinceID = 15, Name = "上饶市"},
                    new City { CityIndex = 1, ProvinceID = 16, Name = "济南市"},
                    new City { CityIndex = 2, ProvinceID = 16, Name = "青岛市"},
                    new City { CityIndex = 3, ProvinceID = 16, Name = "淄博市"},
                    new City { CityIndex = 4, ProvinceID = 16, Name = "枣庄市"},
                    new City { CityIndex = 5, ProvinceID = 16, Name = "东营市"},
                    new City { CityIndex = 6, ProvinceID = 16, Name = "烟台市"},
                    new City { CityIndex = 7, ProvinceID = 16, Name = "潍坊市"},
                    new City { CityIndex = 8, ProvinceID = 16, Name = "济宁市"},
                    new City { CityIndex = 9, ProvinceID = 16, Name = "泰安市"},
                    new City { CityIndex = 10, ProvinceID = 16, Name = "威海市"},
                    new City { CityIndex = 11, ProvinceID = 16, Name = "日照市"},
                    new City { CityIndex = 12, ProvinceID = 16, Name = "莱芜市"},
                    new City { CityIndex = 13, ProvinceID = 16, Name = "临沂市"},
                    new City { CityIndex = 14, ProvinceID = 16, Name = "德州市"},
                    new City { CityIndex = 15, ProvinceID = 16, Name = "聊城市"},
                    new City { CityIndex = 16, ProvinceID = 16, Name = "滨州市"},
                    new City { CityIndex = 17, ProvinceID = 16, Name = "菏泽市"},
                    new City { CityIndex = 1, ProvinceID = 17, Name = "郑州市"},
                    new City { CityIndex = 2, ProvinceID = 17, Name = "开封市"},
                    new City { CityIndex = 3, ProvinceID = 17, Name = "洛阳市"},
                    new City { CityIndex = 4, ProvinceID = 17, Name = "平顶山市"},
                    new City { CityIndex = 5, ProvinceID = 17, Name = "安阳市"},
                    new City { CityIndex = 6, ProvinceID = 17, Name = "鹤壁市"},
                    new City { CityIndex = 7, ProvinceID = 17, Name = "新乡市"},
                    new City { CityIndex = 8, ProvinceID = 17, Name = "焦作市"},
                    new City { CityIndex = 9, ProvinceID = 17, Name = "濮阳市"},
                    new City { CityIndex = 10, ProvinceID = 17, Name = "许昌市"},
                    new City { CityIndex = 11, ProvinceID = 17, Name = "漯河市"},
                    new City { CityIndex = 12, ProvinceID = 17, Name = "三门峡市"},
                    new City { CityIndex = 13, ProvinceID = 17, Name = "南阳市"},
                    new City { CityIndex = 14, ProvinceID = 17, Name = "商丘市"},
                    new City { CityIndex = 15, ProvinceID = 17, Name = "信阳市"},
                    new City { CityIndex = 16, ProvinceID = 17, Name = "周口市"},
                    new City { CityIndex = 17, ProvinceID = 17, Name = "驻马店市"},
                    new City { CityIndex = 18, ProvinceID = 17, Name = "济源市"},
                    new City { CityIndex = 1, ProvinceID = 18, Name = "武汉市"},
                    new City { CityIndex = 2, ProvinceID = 18, Name = "黄石市"},
                    new City { CityIndex = 3, ProvinceID = 18, Name = "十堰市"},
                    new City { CityIndex = 4, ProvinceID = 18, Name = "荆州市"},
                    new City { CityIndex = 5, ProvinceID = 18, Name = "宜昌市"},
                    new City { CityIndex = 6, ProvinceID = 18, Name = "襄樊市"},
                    new City { CityIndex = 7, ProvinceID = 18, Name = "鄂州市"},
                    new City { CityIndex = 8, ProvinceID = 18, Name = "荆门市"},
                    new City { CityIndex = 9, ProvinceID = 18, Name = "孝感市"},
                    new City { CityIndex = 10, ProvinceID = 18, Name = "黄冈市"},
                    new City { CityIndex = 11, ProvinceID = 18, Name = "咸宁市"},
                    new City { CityIndex = 12, ProvinceID = 18, Name = "随州市"},
                    new City { CityIndex = 13, ProvinceID = 18, Name = "仙桃市"},
                    new City { CityIndex = 14, ProvinceID = 18, Name = "天门市"},
                    new City { CityIndex = 15, ProvinceID = 18, Name = "潜江市"},
                    new City { CityIndex = 16, ProvinceID = 18, Name = "神农架林区"},
                    new City { CityIndex = 17, ProvinceID = 18, Name = "恩施土家族苗族自治州"},
                    new City { CityIndex = 1, ProvinceID = 19, Name = "长沙市"},
                    new City { CityIndex = 2, ProvinceID = 19, Name = "株洲市"},
                    new City { CityIndex = 3, ProvinceID = 19, Name = "湘潭市"},
                    new City { CityIndex = 4, ProvinceID = 19, Name = "衡阳市"},
                    new City { CityIndex = 5, ProvinceID = 19, Name = "邵阳市"},
                    new City { CityIndex = 6, ProvinceID = 19, Name = "岳阳市"},
                    new City { CityIndex = 7, ProvinceID = 19, Name = "常德市"},
                    new City { CityIndex = 8, ProvinceID = 19, Name = "张家界市"},
                    new City { CityIndex = 9, ProvinceID = 19, Name = "益阳市"},
                    new City { CityIndex = 10, ProvinceID = 19, Name = "郴州市"},
                    new City { CityIndex = 11, ProvinceID = 19, Name = "永州市"},
                    new City { CityIndex = 12, ProvinceID = 19, Name = "怀化市"},
                    new City { CityIndex = 13, ProvinceID = 19, Name = "娄底市"},
                    new City { CityIndex = 14, ProvinceID = 19, Name = "湘西土家族苗族自治州"},
                    new City { CityIndex = 1, ProvinceID = 20, Name = "广州市"},
                    new City { CityIndex = 2, ProvinceID = 20, Name = "深圳市"},
                    new City { CityIndex = 3, ProvinceID = 20, Name = "珠海市"},
                    new City { CityIndex = 4, ProvinceID = 20, Name = "汕头市"},
                    new City { CityIndex = 5, ProvinceID = 20, Name = "韶关市"},
                    new City { CityIndex = 6, ProvinceID = 20, Name = "佛山市"},
                    new City { CityIndex = 7, ProvinceID = 20, Name = "江门市"},
                    new City { CityIndex = 8, ProvinceID = 20, Name = "湛江市"},
                    new City { CityIndex = 9, ProvinceID = 20, Name = "茂名市"},
                    new City { CityIndex = 10, ProvinceID = 20, Name = "肇庆市"},
                    new City { CityIndex = 11, ProvinceID = 20, Name = "惠州市"},
                    new City { CityIndex = 12, ProvinceID = 20, Name = "梅州市"},
                    new City { CityIndex = 13, ProvinceID = 20, Name = "汕尾市"},
                    new City { CityIndex = 14, ProvinceID = 20, Name = "河源市"},
                    new City { CityIndex = 15, ProvinceID = 20, Name = "阳江市"},
                    new City { CityIndex = 16, ProvinceID = 20, Name = "清远市"},
                    new City { CityIndex = 17, ProvinceID = 20, Name = "东莞市"},
                    new City { CityIndex = 18, ProvinceID = 20, Name = "中山市"},
                    new City { CityIndex = 19, ProvinceID = 20, Name = "潮州市"},
                    new City { CityIndex = 20, ProvinceID = 20, Name = "揭阳市"},
                    new City { CityIndex = 21, ProvinceID = 20, Name = "云浮市"},
                    new City { CityIndex = 1, ProvinceID = 21, Name = "兰州市"},
                    new City { CityIndex = 2, ProvinceID = 21, Name = "金昌市"},
                    new City { CityIndex = 3, ProvinceID = 21, Name = "白银市"},
                    new City { CityIndex = 4, ProvinceID = 21, Name = "天水市"},
                    new City { CityIndex = 5, ProvinceID = 21, Name = "嘉峪关市"},
                    new City { CityIndex = 6, ProvinceID = 21, Name = "武威市"},
                    new City { CityIndex = 7, ProvinceID = 21, Name = "张掖市"},
                    new City { CityIndex = 8, ProvinceID = 21, Name = "平凉市"},
                    new City { CityIndex = 9, ProvinceID = 21, Name = "酒泉市"},
                    new City { CityIndex = 10, ProvinceID = 21, Name = "庆阳市"},
                    new City { CityIndex = 11, ProvinceID = 21, Name = "定西市"},
                    new City { CityIndex = 12, ProvinceID = 21, Name = "陇南市"},
                    new City { CityIndex = 13, ProvinceID = 21, Name = "临夏回族自治州"},
                    new City { CityIndex = 14, ProvinceID = 21, Name = "甘南藏族自治州"},
                    new City { CityIndex = 1, ProvinceID = 22, Name = "成都市"},
                    new City { CityIndex = 2, ProvinceID = 22, Name = "自贡市"},
                    new City { CityIndex = 3, ProvinceID = 22, Name = "攀枝花市"},
                    new City { CityIndex = 4, ProvinceID = 22, Name = "泸州市"},
                    new City { CityIndex = 5, ProvinceID = 22, Name = "德阳市"},
                    new City { CityIndex = 6, ProvinceID = 22, Name = "绵阳市"},
                    new City { CityIndex = 7, ProvinceID = 22, Name = "广元市"},
                    new City { CityIndex = 8, ProvinceID = 22, Name = "遂宁市"},
                    new City { CityIndex = 9, ProvinceID = 22, Name = "内江市"},
                    new City { CityIndex = 10, ProvinceID = 22, Name = "乐山市"},
                    new City { CityIndex = 11, ProvinceID = 22, Name = "南充市"},
                    new City { CityIndex = 12, ProvinceID = 22, Name = "眉山市"},
                    new City { CityIndex = 13, ProvinceID = 22, Name = "宜宾市"},
                    new City { CityIndex = 14, ProvinceID = 22, Name = "广安市"},
                    new City { CityIndex = 15, ProvinceID = 22, Name = "达州市"},
                    new City { CityIndex = 16, ProvinceID = 22, Name = "雅安市"},
                    new City { CityIndex = 17, ProvinceID = 22, Name = "巴中市"},
                    new City { CityIndex = 18, ProvinceID = 22, Name = "资阳市"},
                    new City { CityIndex = 19, ProvinceID = 22, Name = "阿坝藏族羌族自治州"},
                    new City { CityIndex = 20, ProvinceID = 22, Name = "甘孜藏族自治州"},
                    new City { CityIndex = 21, ProvinceID = 22, Name = "凉山彝族自治州"},
                    new City { CityIndex = 1, ProvinceID = 23, Name = "贵阳市"},
                    new City { CityIndex = 2, ProvinceID = 23, Name = "六盘水市"},
                    new City { CityIndex = 3, ProvinceID = 23, Name = "遵义市"},
                    new City { CityIndex = 4, ProvinceID = 23, Name = "安顺市"},
                    new City { CityIndex = 5, ProvinceID = 23, Name = "铜仁地区"},
                    new City { CityIndex = 6, ProvinceID = 23, Name = "毕节地区"},
                    new City { CityIndex = 7, ProvinceID = 23, Name = "黔西南布依族苗族自治州"},
                    new City { CityIndex = 8, ProvinceID = 23, Name = "黔东南苗族侗族自治州"},
                    new City { CityIndex = 9, ProvinceID = 23, Name = "黔南布依族苗族自治州"},
                    new City { CityIndex = 1, ProvinceID = 24, Name = "海口市"},
                    new City { CityIndex = 2, ProvinceID = 24, Name = "三亚市"},
                    new City { CityIndex = 3, ProvinceID = 24, Name = "五指山市"},
                    new City { CityIndex = 4, ProvinceID = 24, Name = "琼海市"},
                    new City { CityIndex = 5, ProvinceID = 24, Name = "儋州市"},
                    new City { CityIndex = 6, ProvinceID = 24, Name = "文昌市"},
                    new City { CityIndex = 7, ProvinceID = 24, Name = "万宁市"},
                    new City { CityIndex = 8, ProvinceID = 24, Name = "东方市"},
                    new City { CityIndex = 9, ProvinceID = 24, Name = "澄迈县"},
                    new City { CityIndex = 10, ProvinceID = 24, Name = "定安县"},
                    new City { CityIndex = 11, ProvinceID = 24, Name = "屯昌县"},
                    new City { CityIndex = 12, ProvinceID = 24, Name = "临高县"},
                    new City { CityIndex = 13, ProvinceID = 24, Name = "白沙黎族自治县"},
                    new City { CityIndex = 14, ProvinceID = 24, Name = "昌江黎族自治县"},
                    new City { CityIndex = 15, ProvinceID = 24, Name = "乐东黎族自治县"},
                    new City { CityIndex = 16, ProvinceID = 24, Name = "陵水黎族自治县"},
                    new City { CityIndex = 17, ProvinceID = 24, Name = "保亭黎族苗族自治县"},
                    new City { CityIndex = 18, ProvinceID = 24, Name = "琼中黎族苗族自治县"},
                    new City { CityIndex = 1, ProvinceID = 25, Name = "昆明市"},
                    new City { CityIndex = 2, ProvinceID = 25, Name = "曲靖市"},
                    new City { CityIndex = 3, ProvinceID = 25, Name = "玉溪市"},
                    new City { CityIndex = 4, ProvinceID = 25, Name = "保山市"},
                    new City { CityIndex = 5, ProvinceID = 25, Name = "昭通市"},
                    new City { CityIndex = 6, ProvinceID = 25, Name = "丽江市"},
                    new City { CityIndex = 7, ProvinceID = 25, Name = "思茅市"},
                    new City { CityIndex = 8, ProvinceID = 25, Name = "临沧市"},
                    new City { CityIndex = 9, ProvinceID = 25, Name = "文山壮族苗族自治州"},
                    new City { CityIndex = 10, ProvinceID = 25, Name = "红河哈尼族彝族自治州"},
                    new City { CityIndex = 11, ProvinceID = 25, Name = "西双版纳傣族自治州"},
                    new City { CityIndex = 12, ProvinceID = 25, Name = "楚雄彝族自治州"},
                    new City { CityIndex = 13, ProvinceID = 25, Name = "大理白族自治州"},
                    new City { CityIndex = 14, ProvinceID = 25, Name = "德宏傣族景颇族自治州"},
                    new City { CityIndex = 15, ProvinceID = 25, Name = "怒江傈傈族自治州"},
                    new City { CityIndex = 16, ProvinceID = 25, Name = "迪庆藏族自治州"},
                    new City { CityIndex = 1, ProvinceID = 26, Name = "西宁市"},
                    new City { CityIndex = 2, ProvinceID = 26, Name = "海东地区"},
                    new City { CityIndex = 3, ProvinceID = 26, Name = "海北藏族自治州"},
                    new City { CityIndex = 4, ProvinceID = 26, Name = "黄南藏族自治州"},
                    new City { CityIndex = 5, ProvinceID = 26, Name = "海南藏族自治州"},
                    new City { CityIndex = 6, ProvinceID = 26, Name = "果洛藏族自治州"},
                    new City { CityIndex = 7, ProvinceID = 26, Name = "玉树藏族自治州"},
                    new City { CityIndex = 8, ProvinceID = 26, Name = "海西蒙古族藏族自治州"},
                    new City { CityIndex = 1, ProvinceID = 27, Name = "西安市"},
                    new City { CityIndex = 2, ProvinceID = 27, Name = "铜川市"},
                    new City { CityIndex = 3, ProvinceID = 27, Name = "宝鸡市"},
                    new City { CityIndex = 4, ProvinceID = 27, Name = "咸阳市"},
                    new City { CityIndex = 5, ProvinceID = 27, Name = "渭南市"},
                    new City { CityIndex = 6, ProvinceID = 27, Name = "延安市"},
                    new City { CityIndex = 7, ProvinceID = 27, Name = "汉中市"},
                    new City { CityIndex = 8, ProvinceID = 27, Name = "榆林市"},
                    new City { CityIndex = 9, ProvinceID = 27, Name = "安康市"},
                    new City { CityIndex = 10, ProvinceID = 27, Name = "商洛市"},
                    new City { CityIndex = 1, ProvinceID = 28, Name = "南宁市"},
                    new City { CityIndex = 2, ProvinceID = 28, Name = "柳州市"},
                    new City { CityIndex = 3, ProvinceID = 28, Name = "桂林市"},
                    new City { CityIndex = 4, ProvinceID = 28, Name = "梧州市"},
                    new City { CityIndex = 5, ProvinceID = 28, Name = "北海市"},
                    new City { CityIndex = 6, ProvinceID = 28, Name = "防城港市"},
                    new City { CityIndex = 7, ProvinceID = 28, Name = "钦州市"},
                    new City { CityIndex = 8, ProvinceID = 28, Name = "贵港市"},
                    new City { CityIndex = 9, ProvinceID = 28, Name = "玉林市"},
                    new City { CityIndex = 10, ProvinceID = 28, Name = "百色市"},
                    new City { CityIndex = 11, ProvinceID = 28, Name = "贺州市"},
                    new City { CityIndex = 12, ProvinceID = 28, Name = "河池市"},
                    new City { CityIndex = 13, ProvinceID = 28, Name = "来宾市"},
                    new City { CityIndex = 14, ProvinceID = 28, Name = "崇左市"},
                    new City { CityIndex = 1, ProvinceID = 29, Name = "拉萨市"},
                    new City { CityIndex = 2, ProvinceID = 29, Name = "那曲地区"},
                    new City { CityIndex = 3, ProvinceID = 29, Name = "昌都地区"},
                    new City { CityIndex = 4, ProvinceID = 29, Name = "山南地区"},
                    new City { CityIndex = 5, ProvinceID = 29, Name = "日喀则地区"},
                    new City { CityIndex = 6, ProvinceID = 29, Name = "阿里地区"},
                    new City { CityIndex = 7, ProvinceID = 29, Name = "林芝地区"},
                    new City { CityIndex = 1, ProvinceID = 30, Name = "银川市"},
                    new City { CityIndex = 2, ProvinceID = 30, Name = "石嘴山市"},
                    new City { CityIndex = 3, ProvinceID = 30, Name = "吴忠市"},
                    new City { CityIndex = 4, ProvinceID = 30, Name = "固原市"},
                    new City { CityIndex = 5, ProvinceID = 30, Name = "中卫市"},
                    new City { CityIndex = 1, ProvinceID = 31, Name = "乌鲁木齐市"},
                    new City { CityIndex = 2, ProvinceID = 31, Name = "克拉玛依市"},
                    new City { CityIndex = 3, ProvinceID = 31, Name = "石河子市"},
                    new City { CityIndex = 4, ProvinceID = 31, Name = "阿拉尔市"},
                    new City { CityIndex = 5, ProvinceID = 31, Name = "图木舒克市"},
                    new City { CityIndex = 6, ProvinceID = 31, Name = "五家渠市"},
                    new City { CityIndex = 7, ProvinceID = 31, Name = "吐鲁番市"},
                    new City { CityIndex = 8, ProvinceID = 31, Name = "阿克苏市"},
                    new City { CityIndex = 9, ProvinceID = 31, Name = "喀什市"},
                    new City { CityIndex = 10, ProvinceID = 31, Name = "哈密市"},
                    new City { CityIndex = 11, ProvinceID = 31, Name = "和田市"},
                    new City { CityIndex = 12, ProvinceID = 31, Name = "阿图什市"},
                    new City { CityIndex = 13, ProvinceID = 31, Name = "库尔勒市"},
                    new City { CityIndex = 14, ProvinceID = 31, Name = "昌吉市"},
                    new City { CityIndex = 15, ProvinceID = 31, Name = "阜康市"},
                    new City { CityIndex = 16, ProvinceID = 31, Name = "米泉市"},
                    new City { CityIndex = 17, ProvinceID = 31, Name = "博乐市"},
                    new City { CityIndex = 18, ProvinceID = 31, Name = "伊宁市"},
                    new City { CityIndex = 19, ProvinceID = 31, Name = "奎屯市"},
                    new City { CityIndex = 20, ProvinceID = 31, Name = "塔城市"},
                    new City { CityIndex = 21, ProvinceID = 31, Name = "乌苏市"},
                    new City { CityIndex = 22, ProvinceID = 31, Name = "阿勒泰市"},
                    new City { CityIndex = 1, ProvinceID = 32, Name = "呼和浩特市"},
                    new City { CityIndex = 2, ProvinceID = 32, Name = "包头市"},
                    new City { CityIndex = 3, ProvinceID = 32, Name = "乌海市"},
                    new City { CityIndex = 4, ProvinceID = 32, Name = "赤峰市"},
                    new City { CityIndex = 5, ProvinceID = 32, Name = "通辽市"},
                    new City { CityIndex = 6, ProvinceID = 32, Name = "鄂尔多斯市"},
                    new City { CityIndex = 7, ProvinceID = 32, Name = "呼伦贝尔市"},
                    new City { CityIndex = 8, ProvinceID = 32, Name = "巴彦淖尔市"},
                    new City { CityIndex = 9, ProvinceID = 32, Name = "乌兰察布市"},
                    new City { CityIndex = 10, ProvinceID = 32, Name = "锡林郭勒盟"},
                    new City { CityIndex = 11, ProvinceID = 32, Name = "兴安盟"},
                    new City { CityIndex = 12, ProvinceID = 32, Name = "阿拉善盟"},
                    new City { CityIndex = 1, ProvinceID = 33, Name = "澳门特别行政区"},
                    new City { CityIndex = 1, ProvinceID = 34, Name = "香港特别行政区"},
            });
            // Submit data into Database
            await _dbContext.SaveChangesAsync();
        }
        #region --- Private functions ---
        private string ProductThumbImagePath(Guid productID, string productImage)
        {
            return ProductThumbImagePath(productID.ToString("D").ToUpper(), productImage);
        }
        private string ProductThumbImagePath(string productID, string productImage)
        {
            return Path.Combine(_productRootPath, productID, productImage);
        }
        private string ProductDetailImagePath(Guid productID, string productImage)
        {
            return ProductDetailImagePath(productID.ToString("D").ToUpper(), productImage);
        }
        private string ProductDetailImagePath(string productID, string productImage)
        {
            return Path.Combine(_productRootPath, productID, "details", productImage).Replace("\\", "/");
        }
        private string BrandImagePath(string brandImage)
        {
            return Path.Combine(_brandRootPath, brandImage);
        }
        #endregion
    }
}
