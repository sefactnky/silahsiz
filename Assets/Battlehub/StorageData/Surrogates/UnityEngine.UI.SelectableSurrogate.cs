using ProtoBuf;
using System;
using System.Threading.Tasks;

namespace Battlehub.Storage.Surrogates.UnityEngine.UI
{
    [ProtoContract]
    [Surrogate(typeof(global::UnityEngine.UI.Selectable), _PROPERTY_INDEX, _TYPE_INDEX)]
    public class SelectableSurrogate<TID> : ISurrogate<TID> where TID : IEquatable<TID>
    {   
        const int _PROPERTY_INDEX = 12;
        const int _TYPE_INDEX = 4125;

        //_PLACEHOLDER_FOR_EXTENSIONS_DO_NOT_DELETE_OR_CHANGE_THIS_LINE_PLEASE

        [ProtoMember(2)]
        public TID id { get; set; }

        [ProtoMember(3)]
        public TID gameObjectId { get; set; }

        [ProtoMember(4)]
        public global::Battlehub.Storage.Surrogates.UnityEngine.UI.NavigationSurrogate<TID> navigation { get; set; }

        [ProtoMember(5)]
        public global::UnityEngine.UI.Selectable.Transition transition { get; set; }

        [ProtoMember(6)]
        public global::UnityEngine.UI.ColorBlock colors { get; set; }

        [ProtoMember(7)]
        public global::Battlehub.Storage.Surrogates.UnityEngine.UI.SpriteStateSurrogate<TID> spriteState { get; set; }

        [ProtoMember(8)]
        public TID animationTriggers { get; set; }

        [ProtoMember(9)]
        public TID targetGraphic { get; set; }

        [ProtoMember(10)]
        public global::System.Boolean interactable { get; set; }

        [ProtoMember(11)]
        public TID image { get; set; }

        [ProtoMember(12)]
        public global::System.Boolean enabled { get; set; }

        //_PLACEHOLDER_FOR_NEW_PROPERTIES_DO_NOT_DELETE_OR_CHANGE_THIS_LINE_PLEASE

        public ValueTask Serialize(object obj, ISerializationContext<TID> ctx)
        {
            var idmap = ctx.IDMap;

            var o = (global::UnityEngine.UI.Selectable)obj;
            id = idmap.GetOrCreateID(o);
            gameObjectId = idmap.GetOrCreateID(o.gameObject);
            navigation.Serialize(o.navigation, ctx);
            transition = o.transition;
            colors = o.colors;
            spriteState.Serialize(o.spriteState, ctx);
            animationTriggers = idmap.GetOrCreateID(o.animationTriggers);
            targetGraphic = idmap.GetOrCreateID(o.targetGraphic);
            interactable = o.interactable;
            image = idmap.GetOrCreateID(o.image);
            enabled = o.enabled;
            //_PLACEHOLDER_FOR_SERIALIZE_METHOD_BODY_DO_NOT_DELETE_OR_CHANGE_THIS_LINE_PLEASE

            return default;
        }

        public ValueTask<object> Deserialize(ISerializationContext<TID> ctx)
        {
            var idmap = ctx.IDMap;

            var o = idmap.GetComponent<global::UnityEngine.UI.Selectable, TID>(id, gameObjectId);
            o.navigation = navigation.Deserialize(ctx);
            o.transition = transition;
            o.colors = colors;
            o.spriteState = spriteState.Deserialize(ctx);
            o.animationTriggers = idmap.GetObject<global::UnityEngine.UI.AnimationTriggers>(animationTriggers);
            o.targetGraphic = idmap.GetObject<global::UnityEngine.UI.Graphic>(targetGraphic);
            o.interactable = interactable;
            o.image = idmap.GetObject<global::UnityEngine.UI.Image>(image);
            o.enabled = enabled;
            //_PLACEHOLDER_FOR_DESERIALIZE_METHOD_BODY_DO_NOT_DELETE_OR_CHANGE_THIS_LINE_PLEASE

            return new ValueTask<object>(o);
        }
    }
}
