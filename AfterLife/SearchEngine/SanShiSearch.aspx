<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SanShiSearch.aspx.vb" Inherits="AfterLife_SearchEngine_SanShiSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <base target='_blank' />
    <!--<script src="<%=ResolveClientUrl("~/JS/jquery.scrollTo.js")%>" type="text/javascript"></script>-->
    <script src="<%=ResolveClientUrl("~/JS/SanshiSearchWeb.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section style="padding-bottom: 20px;">
        <div class="container">
            <div id="KeywordDiv" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">紧张的搜索引擎 ( Tense Search Engine )</h2>
                </div>
                <div style="text-align: left" class="InputDIV">
                    请输入关键字：
                    <input type="text" id="Text1" runat="server" class="WantKW" onkeypress="IsPressEnter(event)" />
                    <input type="button" id="Button1" runat="server" class="btn btn-info" value="Go" onclick="IsPressEnter(event)" />
                </div>
            </div>
            <div id="ShowResult" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                <%--                <div class="ShowEveryResultInfo" id="SERI1">
                    <h3 class="ResultHead">
                        <a href="https://jakubmarian.com/many-money-vs-much-money-in-english/">Many money vs much money  in English - Jakub Marian</a>
                    </h3>
                    <div class="ResultContent">
                        Just like water, sugar, or love, <em>money</em> (in its most common sense) is an uncountable noun. This means, in
        particular, that we can't have "a.
                    </div>
                    <div class="ResultLink" >
                        <cite class="_Rm">https://jakubmarian.com/<b>many</b>-<b>money</b>-vs-<b>much</b>-<b>money</b>-in-engl...</cite>
                    </div>
                </div>

                <div class="ShowEveryResultInfo" id="SERI2">
                    <h3 class="ResultHead">
                        <a href="https://jakubmarian.com/many-money-vs-much-money-in-english/">Many <em>money</em> vs much money  in English - Jakub Marian</a>
                    </h3>
                    <div class="ResultContent">
                        Just like water, sugar, or love, <em>money</em> (in its most common sense) is an uncountable noun. This means, in
        particular, that we can't have "a.
                    </div>
                    <div class="ResultLink" style="white-space: nowrap">
                        <cite class="_Rm">https://jakubmarian.com/<b>many</b>-<b>money</b>-vs-<b>much</b>-<b>money</b>-in-engl...</cite>
                        <span class="QuoteFrom">&nbsp&nbsp&nbsp 结果来自 Google</span>

                    </div>

                </div>


                <div class="ShowEveryResultInfo" id="SERI3">
                    <h3 class="ResultHead">
                        <a href="http://dianshiju.cntv.cn/special/mzd/">&#x7535;&#x89C6;&#x5267;&#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B;_&#x7535;&#x89C6;&#x5267;_&#x592E;&#x89C6;&#x7F51;(cctv.com)</a>
                    </h3>
                    <div class="ResultContent">
                        &#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B; &#x7B2C;1 &#x96C6; &#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B; &#x7B2C;2&#x96C6; &#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B; &#x7B2C;3&#x96C6; &#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B; &#x7B2C;4&#x96C6; &#x300A;<em>&#x6BDB;&#x6CFD;&#x4E1C;</em>&#x300B; &#x7B2C;5&#x96C6; ... <em>&#x6211;&#x7231;</em> &#x65B0;&#x7586;&#x2014;&#x6751;&#x91CC;&#x6709;&#x4E2A;&#x59D1;&#x5A18;&#x53EB;&#x5C0F;&#x82B3;&#xFF08;&#x7CFB;&#x5217;&#x5267;&#xFF09; &#x5367;&#x5E95;&#xFF08;&#x680F;&#x76EE;&#x5267;&#xFF09; &#x6211;&#x4E0D;&#x662F;&#x574F;&#x86CB; ...
                    <br />
                        这个是Bing的 ，需要把Strong替换成em
                    </div>
                    <div class="ResultLink" style="white-space: nowrap">
                        <cite class="_Rm">dianshiju.cntv.cn/special/mzd</cite>
                        <span class="QuoteFrom">&nbsp&nbsp&nbsp结果来自 Bing</span>

                    </div>

                </div>

                <div class="ShowEveryResultInfo" id="SERI4">
                    <h3 class="ResultHead">
                        <a href="http://www.baidu.com/link?url=u6ZhKiZYsGyXEKXu_YH0H7d_HNLL-DsQYaNulgo-OEFms3-2BacxWXFArMZtgb5zFTL_6i9dIc-cQLWE3kYMFsgk9_Ml7NaJrKHGh5sf_83">&#x535A;&#x6587;_<em>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</em>_&#x65B0;&#x6D6A;&#x535A;&#x5BA2;</a>
                    </h3>
                    <div class="ResultContent">
                        &#x535A;&#x6587;_<em>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</em>_&#x65B0;&#x6D6A;&#x535A;&#x5BA2;,<em>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</em>,[&#x8F6C;&#x8F7D;]&#x4E1A;&#x62A5;&#x5F97;&#x75C5;:&#x4F5B;&#x4E4B;&#x4E5D;&#x96BE;,[&#x8F6C;&#x8F7D;]&#x8F6C;:73&#x5C81;&#x6BDB;&#x6CFD;&#x4E1C;&#x53D1;&#x52A8;&#x201C;&#x6587;&#x9769;&#x201D;:&#x4E0D;&#x60F3;&#x8BA9;&#x8001;&#x767E;&#x59D3;&#x518D;&#x56DE;&#x89E3;&#x653E;&#x524D;,[&#x8F6C;&#x8F7D;]&#x60F3;&#x4E0D;&#x5230;&#x4EBA;&#x5BB6;&#x4EB2;&#x81EA;&#x6765;&#x5317;&#x4EAC;&#x201C;...
                    <br />
                        这个是百度的
                    </div>
                    <div class="ResultLink" style="white-space: nowrap">
                        <cite class="_Rm">blog.sina.com.cn/s/art...&#xA0;</cite>
                        <span class="QuoteFrom">&nbsp&nbsp&nbsp结果来自 Baidu</span>

                    </div>

                </div>

                <div class="ShowEveryResultInfo" id="SERI5">
                    <h3 class="ResultHead">
                        <a href="https://www.google.com.hk/url?q=https://zh.wikipedia.org/zh-hk/User_talk:%25E6%2588%2591%25E7%2588%25B1%25E6%25AF%259B%25E6%25B3%25BD%25E4%25B8%259C&sa=U&ved=0ahUKEwjQ9r3OlcvOAhVX4GMKHapzBpMQFggfMAE&usg=AFQjCNEsIOWqLfxJ31JPhUxYbnWBdsS0pg">User talk:<em>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</em>- &#x7EF4;&#x57FA;&#x767E;&#x79D1;&#xFF0C;&#x81EA;&#x7531;&#x7684;&#x767E;&#x79D1;&#x5168;&#x4E66;</a>
                    </h3>
                    <div class="ResultContent">
                        &#x60A8;&#x597D;&#xFF0C;<em>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</em>&#xFF01;&#x6B22;&#x8FCE;&#x52A0;&#x5165;&#x7EF4;&#x57FA;&#x767E;&#x79D1;&#xFF01; &#x611F;&#x8B1D;&#x60A8;&#x5C0D;&#x7DAD;&#x57FA;&#x767E;&#x79D1;&#x7684;&#x8208;&#x8DA3;&#x8207;&#x8CA2;&#x737B;&#xFF0C;&#x5E0C;&#x671B;&#x60A8;&#x6703;<br>
                        \n&#x559C;&#x6B61;&#x9019;&#x88E1;&#x3002;&#x9664;&#x4E86;&#x6B61;&#x8FCE;&#x8FAD;&#x4EE5;&#x5916;&#xFF0C;&#x4E5F;&#x8ACB;&#x60A8;&#x4E86;&#x89E3;&#x4EE5;&#x4E0B;&#x91CD;&#x8981;&#x6587;&#x7AE0;&#xFF1A;&#xA0;...
                    <br />
                        这个是Google的，要把b替换成em，而且记得URL要把原始地址提取出来<br />
                        全部显示地址栏的strong都替换成b<br />
                        全部显示内容以及标题的都替换成em
                    </div>
                    <div class="ResultLink" style="white-space: nowrap">
                        <cite class="_Rm">https://zh.wikipedia.org/zh-hk/User_talk:<b>&#x6211;&#x7231;&#x6BDB;&#x6CFD;&#x4E1C;</b></cite>
                        <span class="QuoteFrom">&nbsp&nbsp&nbsp结果来自 Baidu</span>

                    </div>

                </div>--%>
            </div>
                        <div class="CannotFind col-lg-12 col-md-12 col-sm-12 col-xs-12" id="CannotFind">
                <h2>正在为你找查结果哟~<br />
                    <img src="/IMG/Loading.gif" width="10%" /></h2>
            </div>
            <div style="text-align: right" class="ShowResultCount col-lg-12 col-md-12 col-sm-12 col-xs-12">
                哎哟~为你找到 XX 条记录了！
            </div>
            <div class="NOPage col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <input type="button" value="<前一页" id="PreviousPage" class="btn" style="display:none;" />
                
                <span id="ContentPage" style="position:static">

                </span>
                <input type="button" value="后一页>" id="NextPage" class="btn" style="display:none;"  />

            </div>



        </div>
    </section>
</asp:Content>

