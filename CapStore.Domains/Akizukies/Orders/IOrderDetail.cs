﻿namespace CapStore.Domains.Akizukies.Orders;

/// <summary>
/// 秋月電子の注文詳細モデル
/// </summary>
public interface IOrderDetail
{
    /// <summary>
    /// 注文ID
    /// </summary>
    public OrderId OrderId { get; }
    /// <summary>
    /// 注文日
    /// </summary>
    public OrderDate OrderDate { get; }
    /// <summary>
    /// 注文内容詳細
    /// </summary>
    public IReadOnlyCollection<AkizukiOrderComponent> Components { get; }
}
